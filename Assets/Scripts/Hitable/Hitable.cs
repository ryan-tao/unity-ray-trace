using System.Collections.Generic;

namespace RayTrace
{
    public interface IHitable
    {
        bool Hit(Ray ray, float min, float max, ref HitRecord rec);
    }

    public class HitableList
    {
        public List<IHitable> List;
        public HitableList()
        {
            List = new List<IHitable>();
        }

        public bool Hit(Ray ray, float min, float max, ref HitRecord rec)
        {
            var tempRec = new HitRecord();
            var hit = false;
            var cloest = max;
            foreach (var h in List)
            {
                if (h.Hit(ray, min, cloest, ref tempRec))
                {
                    hit = true;
                    cloest = tempRec.Root;
                    rec = tempRec;
                }
            }

            return hit;
        }
    }
}
