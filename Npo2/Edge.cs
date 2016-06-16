namespace Npo2
{
    class Edge
    {
        public Vertex Startpoint { get; private set; }

        public Vertex Endpoint { get; private set; }

        public Edge(Vertex s, Vertex e)
        {
            Startpoint = s;
            Endpoint = e;
        }

    }
}
