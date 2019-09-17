using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KesMemorija.Historical
{
    public class DeltaCD
    {
        private int transactionID;
        private CollectionDescription add;
        private CollectionDescription update;
        private CollectionDescription remove;


        public DeltaCD()
        {

        }

        public int TransactionID { get => transactionID; set => transactionID = value; }
        public CollectionDescription Add { get => add; set => add = value; }
        public CollectionDescription Update { get => update; set => update = value; }
        public CollectionDescription Remove { get => remove; set => remove = value; }
    }
}
