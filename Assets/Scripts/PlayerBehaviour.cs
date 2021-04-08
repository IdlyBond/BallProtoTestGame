using System.Collections;
using AndromedaCore.LevelManagement;
using Cinemachine;
using Lean.Touch;
using TheKnightsOfUnity.LineRendererPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    [SerializeField] private Transform body;
    [SerializeField] private float bulletGrowSpeed;
    [SerializeField] private float playerDecreaseMultiplier;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask finishLayerMask;
    
    
    [SerializeField] private Animator graphicsAnimator;
    [SerializeField] private Animator jumpAnimator;
    
    [SerializeField] private LineRendererPro lineRenderer;

    private bool _isActive;
    private bool _isCreating;
    
    void Awake()
    {
        _isActive = true;
        
        LeanTouch.OnFingerDown += OnFingerDown;
        WorldBroadcast.OnBulletDestroyed.Subscribe(OnBulletDestroyed);
        
        graphicsAnimator.Play("Create");

        FindObjectOfType<CinemachineVirtualCamera>().Follow = body;
    }

    private void OnDestroy()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
        WorldBroadcast.OnBulletDestroyed.Unsubscribe(OnBulletDestroyed);
    }

    private void OnFingerDown(LeanFinger f)
    {
        if (!CanCreateBullet()) return;
        _isCreating = true;
        StartCoroutine(BulletCreatingProcess(f));
    }

    private IEnumerator BulletCreatingProcess(LeanFinger f)
    {
        lineRenderer.gameObject.SetActive(true);
        graphicsAnimator.SetTrigger("Fill");
        var prepareBullet = Instantiate(GameAssets.i.prepareBulletPrefab, 
            body.position + body.forward * body.localScale.x, 
            Quaternion.identity).GetComponentInChildren<PrepareBulletBehaviour>();
        var startSize = 0.09f;
        var size = startSize;
        while (f.Set && !IsDepleted())
        {
            var grow = Time.deltaTime * bulletGrowSpeed;
            size += grow;
            body.localScale -= grow * playerDecreaseMultiplier * Vector3.one;
            prepareBullet.SetSize(size);
            yield return null;
        }
        prepareBullet.Ready();
        graphicsAnimator.SetTrigger("Idle");
        
        if (IsDepleted())
        {
            DisableLoose();
        }
        lineRenderer.gameObject.SetActive(false);
    }

    private void OnBulletDestroyed(int killCount)
    {
        StartCoroutine(MoveTowards());
    }

    private IEnumerator MoveTowards()
    {
        var forwardEnemies = BoxCastForward(enemyLayerMask);
        if (forwardEnemies.Length > 0)
        {
            if (jumpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) jumpAnimator.SetTrigger("Jump");
            
            var smallestZ = forwardEnemies[0].collider.gameObject.transform.position.z;
            foreach (var enemy in forwardEnemies)
            {
                var z = enemy.collider.gameObject.transform.position.z;
                if (z < smallestZ) smallestZ = z;
            }

            while (body.position.z < smallestZ - 5 && _isActive)
            {
                body.Translate(Vector3.forward * (Time.deltaTime * 5f));
                yield return null;
            }
            if (jumpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) jumpAnimator.SetTrigger("Idle");
        }
        else
        {
            if (jumpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) jumpAnimator.SetTrigger("Jump");

            var finishCasts = BoxCastForward(finishLayerMask);
            if (finishCasts.Length > 0)
            {
                var finishZ = finishCasts[0].collider.transform.position.z;
                
                while (body.position.z < finishZ - 2)
                {
                    body.Translate(Vector3.forward * (Time.deltaTime * 4f));
                    yield return null;
                }
            }
            if (_isActive)DisableWin();
            if (jumpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) jumpAnimator.SetTrigger("Idle");
        }
        jumpAnimator.SetTrigger("Idle");
        _isCreating = false;
    }

    private RaycastHit[] BoxCastForward(LayerMask mask)
    {
        return Physics.BoxCastAll(body.position,
            Vector3.right * body.localScale.x / 2f + Vector3.up, Vector3.forward, Quaternion.identity,
            200f, mask);
    }

    private void DisableLoose()
    {
        _isActive = false;
        graphicsAnimator.Play("Died");
        WorldBroadcast.LooseConditionAchieved.Publish(gameObject);
        print("PlayerLoose");
    }

    private void DisableWin()
    {
        _isActive = false;
        WorldBroadcast.WinConditionAchieved.Publish(gameObject);
        print("Win!");
    }

    private bool IsDepleted()
    {
        return body.localScale.x <= 0.3f;
    }

    private bool CanCreateBullet()
    {
        return _isActive && !IsDepleted() && !_isCreating;
    }
}
