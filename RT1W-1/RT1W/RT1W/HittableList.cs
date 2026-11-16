using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class HittableList:Hittable
    {
        List<Hittable> objs = new List<Hittable>();

        public void Clear()
        {
            objs.Clear();
        }
        public void Add(Hittable obj) {
            objs.Add(obj);
        }

        public override HitRecord? Hit(Ray ray, Interval range)
        {
            HitRecord? res = null;
            Interval valid_t = range;
            HitRecord? temp;

            //loop over each object and check if it hits, and keep track of the closest one
            foreach (Hittable obj in objs) {
                temp = obj.Hit(ray, valid_t);
                if (temp is not null) {
                    valid_t.max = temp.t;
                    res = temp;
                }
            }

            //return the closest hit (if there is one lol)
            return res;
        }
    }
}
