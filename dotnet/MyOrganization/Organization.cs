using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private int count; // id for emp

        public Organization()
        {
            root = CreateOrganization();
            count = 0;
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {

            var org = FillPosition(root, title, person);
            count++;

            return org;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private Position? FillPosition(Position pos, string title, Name person)
        {

            if (pos.GetTitle() == title)
            {
                Employee emp = new Employee(count, person);
                pos.SetEmployee(emp);

                return pos;
            }

            foreach (Position p in pos.GetDirectReports())
            {

                var subOrg = FillPosition(p, title, person);
                if(subOrg?.GetTitle() == title)
                {
                    return subOrg;
                }

            }

                return null;
        }


        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
