using UnityEngine;

public class Container : MonoBehaviour
{
    //public float safeZone;
    public float resolution;
    public float threshold;
    public ComputeShader computeShader;
    public bool calculateNormals;
    public MetaBall[] balls;
    public MeshFilter filter;

    private CubeGrid grid;

    public void Start()
    {
        filter = GetComponent<MeshFilter>();
        this.grid = new CubeGrid(this, this.computeShader);
        computeShader = Instantiate(computeShader);
    }

    public void Update()
    {
        this.grid.evaluateAll(balls);

        Mesh mesh = filter.mesh;
        mesh.Clear();
        mesh.vertices = this.grid.vertices.ToArray();
        mesh.triangles = this.grid.getTriangles();

        if (this.calculateNormals)
        {
            mesh.RecalculateNormals();
        }
        mesh.Optimize();
    }
}