using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexaSphere
{
    private struct Triangle
    {
        public int A;
        public int B;
        public int C;

        public Triangle(int a, int b, int c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", A, B, C);
        }
    }

    private static Vector3 SmoothCurve(Vector3 v)
    {
        return SmoothCurve(v.x, v.y, v.z);
    }

    private static Vector3 SmoothCurve(float x, float y, float z)
    {
        var length = Mathf.Sqrt((x * x) + (y * y) + (z * z));
        var smoothPoint = new Vector3(x / length, y / length, z / length);

        return smoothPoint;

        // HACK Remove smoothing curve
        //return new Vector3(x, y, z);
    }

    private static Vector3 GetMiddlePoint(Vector3 p1, Vector3 p2)
    {
        return new Vector3((p1.x + p2.x) / 2f,
                           (p1.y + p2.y) / 2f,
                           (p1.z + p2.z) / 2f);
    }

    private static int[] ConvertToMeshFilterTriangles(Triangle[] triangles)
    {
        var result = new List<int>();

        for (int i = 0; i < triangles.Length; i++)
        {
            result.Add(triangles[i].A);
            result.Add(triangles[i].B);
            result.Add(triangles[i].C);
        }

        return result.ToArray();
    }

    public static Mesh BuildMesh(int recursionLevel, float radius)
    {
        var mesh = new Mesh();

        // create 12 vertices of a icosahedron
        var t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        var vertices = new List<Vector3>(12);
        var uvs = new List<Vector2>(12);

        vertices.Add(SmoothCurve(-1, t, 0));
        vertices.Add(SmoothCurve(1, t, 0));
        vertices.Add(SmoothCurve(-1, -t, 0));
        vertices.Add(SmoothCurve(1, -t, 0));

        vertices.Add(SmoothCurve(0, -1, t));
        vertices.Add(SmoothCurve(0, 1, t));
        vertices.Add(SmoothCurve(0, -1, -t));
        vertices.Add(SmoothCurve(0, 1, -t));

        vertices.Add(SmoothCurve(t, 0, -1));
        vertices.Add(SmoothCurve(t, 0, 1));
        vertices.Add(SmoothCurve(-t, 0, -1));
        vertices.Add(SmoothCurve(-t, 0, 1));

        uvs.Add(new Vector2(0.5f, 0.5f));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));

        uvs.Add(new Vector2(0f, 0.2f));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0.5f, 0.2f));

        Triangle[] triangles = {
            // 5 faces around point 0
            new Triangle(0, 11, 5),
            new Triangle(0,  5,  1),
            new Triangle(0,  1,  7),
            new Triangle(0,  7, 10),
            new Triangle(0, 10, 11),

            // 5 adjacent faces
            new Triangle( 1,  5,  9),
            new Triangle( 5, 11,  4),
            new Triangle(11, 10,  2),
            new Triangle(10,  7,  6),
            new Triangle( 7,  1,  8),

            // 5 faces around point 3
            new Triangle(3, 9, 4),
            new Triangle(3, 4, 2),
            new Triangle(3, 2, 6),
            new Triangle(3, 6, 8),
            new Triangle(3, 8, 9),

            // 5 adjacent faces
            new Triangle(4, 9, 5),
            new Triangle(2, 4, 11),
            new Triangle(6, 2, 10),
            new Triangle(8, 6, 7),
            new Triangle(9, 8, 1)
        };

        for (int i = 0; i < 12; i++)
        {
            vertices[i] *= radius;
            uvs[i] *= radius;
        }

        for (int i = 0; i < recursionLevel; i++)
        {
            var newTriangles = new List<Triangle>();

            foreach (var face in triangles)
            {
                var AB = GetMiddlePoint(vertices[face.A], vertices[face.B]);
                vertices.Add(SmoothCurve(AB) * radius);
                int ab = vertices.Count - 1;

                var BC = GetMiddlePoint(vertices[face.B], vertices[face.C]);
                vertices.Add(SmoothCurve(BC) * radius);
                int bc = vertices.Count - 1;

                var CA = GetMiddlePoint(vertices[face.C], vertices[face.A]);
                vertices.Add(SmoothCurve(CA) * radius);
                int ca = vertices.Count - 1;

                var tri = new Triangle(face.A, ab, ca);

                newTriangles.Add(tri);

                newTriangles.Add(new Triangle(face.B, bc, ab));
                newTriangles.Add(new Triangle(face.C, ca, bc));
                newTriangles.Add(new Triangle(ab, bc, ca));
            }

            triangles = newTriangles.ToArray();
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = ConvertToMeshFilterTriangles(triangles);

        return mesh;
    }
}
