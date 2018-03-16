using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EstructuraDeDatos;
using Laboratorio3ED1.Clases;
using Laboratorio3ED1.Models;

namespace Laboratorio3ED1.Singleton
{
    public class DBconnection
    {
        private static volatile DBconnection instance;

        private static object sync = new Object();
        private static Comparador Comparador_ = new Comparador();

        public AVL<Partido> AVlFecha = new AVL<Partido>(Comparador_.CompareByDate);
        public AVL<Partido> AVLNpartido = new AVL<Partido>(Comparador_.CompareByMatchNumber);
       


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