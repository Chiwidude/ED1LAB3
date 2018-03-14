using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio3ED1.Singleton
{
    public class DBconnection
    {
        private static volatile DBconnection instance;

        private static object sync = new Object();

        public static DBconnection getInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new DBconnection();
                        }
                    }
                }

                return instance;
            }
        }
    }
}