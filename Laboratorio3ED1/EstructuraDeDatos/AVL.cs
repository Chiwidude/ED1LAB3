using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EstructuraDeDatos
{
    public class AVL<T> : IAVL<T> where T: IComparable
    {

        public Nodo<T> root;
        public List<T> mylist;
        CompareTo<T> Comparador;

        #region Constructores

        public AVL()
        {
            root = null;
            
        }
        public AVL(CompareTo<T> compare)
        {
            root = null;
            
            this.Comparador = compare;
        }
        #endregion

        #region Operaciones_Básicas

        public Nodo<T> Buscar(T value)
        {
            var timer = new LogFile();
            var auxiliar = root;
#pragma warning disable CC0031 // Check for null before calling a delegate
            while (Comparador(auxiliar.value,value) != 0)
#pragma warning restore CC0031 // Check for null before calling a delegate
            {
#pragma warning disable CC0031 // Check for null before calling a delegate
                  auxiliar = Comparador(value, auxiliar.value) < 0 ? auxiliar.Izquierdo : auxiliar.Derecho;
                if (auxiliar == null)
                {
                    timer.Logcreate("Búsqueda sin éxito");
                        return null;
                }
            }
            timer.Logcreate("Búsqueda con éxito");
            return auxiliar;
        }

        public Nodo<T> Eliminar(T value)
        {
            var timer = new LogFile();
            var auxiliar = root;
            var padre = null as Nodo<T>;
            var esHijoIz = true;
            while (Comparador(auxiliar.value,value) != 0)
            {
                padre = auxiliar;
                if (Comparador(value, auxiliar.value) < 0)
                {
                    esHijoIz = true;
                    auxiliar = auxiliar.Izquierdo;
                }
                else
                {
                    esHijoIz = false;
                    auxiliar = auxiliar.Derecho;
                }
                if (auxiliar == null)
                {
                    return null;
                }
            }// Fin ciclo inicial

            if (auxiliar.Izquierdo == null && auxiliar.Derecho == null) // nodo hoja
            {
                if (padre != null)
                {

                    if (esHijoIz)
                    {
                        padre.Izquierdo = null;
                        timer.Logcreate("Eliminación");
                    }
                    else
                    {
                        padre.Derecho = null;
                        timer.Logcreate("Eliminación");
                    }
                    Balancear(padre, false, esHijoIz);
                }
                else
                {
                    root = null;
                    timer.Logcreate("Eliminación");
                }
            }
            else if (auxiliar.Derecho == null) // solo hijo Izquierdo
            {
                if (padre != null)
                {
                    if (esHijoIz)
                    {
                        padre.Izquierdo = auxiliar.Izquierdo;
                        auxiliar.Izquierdo.Padre = padre;
                        timer.Logcreate("Eliminación");
                    }
                    else
                    {
                        padre.Derecho = auxiliar.Izquierdo;
                        auxiliar.Izquierdo.Padre = padre;
                        timer.Logcreate("Eliminación");
                    }
                    Balancear(padre, false, esHijoIz);
                }
                else
                {
                    auxiliar.Izquierdo.Padre = null;
                    root = auxiliar.Izquierdo;
                    timer.Logcreate("Eliminación");
                }
            }
            else if (auxiliar.Izquierdo == null) //solo hijo Derecho
            {
                if (padre != null)
                {

                    if (esHijoIz)
                    {
                        padre.Izquierdo = auxiliar.Derecho;
                        auxiliar.Derecho.Padre = padre;
                        timer.Logcreate("Eliminación");
                    }
                    else
                    {
                        padre.Derecho = auxiliar.Derecho;
                        auxiliar.Derecho.Padre = padre;
                        timer.Logcreate("Eliminación");
                    }
                    Balancear(padre, false, esHijoIz);
                }
                else
                {
                    auxiliar.Derecho.Padre = null;
                    root = auxiliar.Derecho;
                    timer.Logcreate("Eliminación");
                }
            }

            else // Tiene dos hijos
            {
                var reemplazo = Reemplazar(auxiliar);
                if (root == auxiliar)
                {
                    root = reemplazo;
                    root.Padre = null;
                    timer.Logcreate("Eliminación");
                    Balancear(root, false, esHijoIz);
                   
                }
                else if (esHijoIz)
                {
                    padre.Izquierdo = reemplazo;
                    padre.Izquierdo.Padre = padre;
                    timer.Logcreate("Eliminación");
                    Balancear(padre, false, esHijoIz);
                }
                else
                {
                    padre.Derecho = reemplazo;
                    timer.Logcreate("Eliminación");
                    Balancear(padre, false, esHijoIz);

                }
                reemplazo.Izquierdo = auxiliar.Izquierdo;
                timer.Logcreate("Eliminación");


            }
                return auxiliar;
       }
        

        /// <summary>
        /// Elimina un Nodo mediante sustitucion
        /// </summary>
        /// <param name="Nodoelmiminar">Nodo a Eliminar </param>
        /// <returns>Nodo de Reemplazo</returns>
        private static Nodo<T> Reemplazar(Nodo<T> Nodoelmiminar)
        {
            var reemplazopadre = Nodoelmiminar;
            var reemplazo = Nodoelmiminar;
            var auxiliar = Nodoelmiminar.Derecho;
            while (auxiliar != null)
            {
                reemplazopadre = reemplazo;
                reemplazo = auxiliar;
                auxiliar = auxiliar.Izquierdo;
            }
            if (reemplazo != Nodoelmiminar.Derecho)
            {
                reemplazopadre.Izquierdo = reemplazo.Derecho;
                reemplazo.Derecho = Nodoelmiminar.Derecho;
            }
            return reemplazo;
        }


        public void Insertar(T value)
        {
            var timer = new LogFile();
            var newnode = new Nodo<T>(value);
            if(root == null)
            {
                root = newnode;
                timer.Logcreate("Inserción");
            } else
            {
                InsertarInterno(newnode, root);
                timer.Logcreate("Inserción");
            }

        }

        private void InsertarInterno(Nodo<T> newnode, Nodo<T> padre)
        {

            if (padre != null)
            {
                if (Comparador(newnode.value,padre.value) < 0)
                {
                    if (padre.Izquierdo == null)
                    {
                        
                        padre.Izquierdo = newnode;
                        padre.Izquierdo.Padre = padre;
                        Balancear(padre, true, true);
                    }
                    else
                    {
                        InsertarInterno(newnode, padre.Izquierdo);
                    }
                }
                else
                {
                    if (Comparador(newnode.value,padre.value) > 0)
                    {
                        if (padre.Derecho == null)
                        {
                            
                            padre.Derecho = newnode;
                            padre.Derecho.Padre = padre;
                            Balancear(padre, true, false);
                        }
                        else
                        {
                            InsertarInterno(newnode, padre.Derecho);
                        }
                    }
                }
            }

        }
        #endregion

        #region Recorridos
        private void PostFijo_(Nodo<T> node)
        {
            if(node != null)
            {
                PostFijo_(node.Izquierdo);
                PostFijo_ (node.Derecho);
                mylist.Add(node.value);
            }
        }

        private void Prefijo_(Nodo<T> node)
        {
            if (node != null)
            {
                mylist.Add(node.value);
                Prefijo_(node.Izquierdo);
                Prefijo_(node.Derecho);
               
            }
        }
        private void Infijo_(Nodo<T> node)
        {
            if (node != null)
            {
                Infijo_(node.Izquierdo);
                mylist.Add(node.value);
                Infijo_(node.Derecho);
            }
        }
       public  List<T> Infijo()
        {
            mylist = new List<T>();
            Infijo_(root);
            return mylist;
        }

       public List<T> Prefijo()
        {
            mylist = new List<T>();
            Prefijo_(root);
            return mylist;
        }

        public List<T> PostFijo()
        {
            mylist = new List<T>();
            PostFijo_(root);
            return mylist;
        }



        #endregion
        #region Rotaciones
        protected void RotaciónDerecha(Nodo<T> nodo)
        {
            var timer = new LogFile();
            var Rootparent = nodo.Padre;
            var Q = nodo as Nodo<T>;
            var P = Q.Izquierdo as Nodo<T>;
            var C = P.Derecho as Nodo<T>;

            if (Rootparent != null)
            {
                if (Rootparent.Derecho == Q)
                {
                    Rootparent.Derecho = P;
                }
                else
                {
                    Rootparent.Izquierdo = P;
                }

            }
            else
            {
                root = P as Nodo<T>;
                root.Padre = null;
            }

            Q.Izquierdo = C;
            P.Derecho = Q;
            Q.Padre = P;

            if(C != null)
                C.Padre = Q;
                P.Padre = Rootparent;
            

            if(P.Factor == 0)
            {
                Q.Factor = -1;
                P.Factor = 1;
            }
            else
            {
                Q.Factor = 0;
                P.Factor = 0;
            }

            timer.Logcreate("Rotación Simple a la Derecha");



        }

        protected void RotaciónIzquierda(Nodo<T> nodo)
        {
            var timer = new LogFile();
            var RootNode = nodo.Padre;
            var P = nodo;
            var Q = P.Derecho;
            var B = Q.Izquierdo;

            if(RootNode != null)
            {
                if(RootNode.Derecho == P)
                {
                    RootNode.Derecho = Q;
                }
                else
                {
                    RootNode.Izquierdo = Q;
                }
            } else
            {
                root = Q;
                root.Padre = null;
            }

            P.Derecho = Q.Izquierdo;
            Q.Izquierdo = P;
            P.Padre = Q;

            if(B != null)
                B.Padre = P;
                Q.Padre = RootNode;

            if(Q.Factor == 0)
            {
                P.Factor = 1;
                Q.Factor = -1;
            }
            else
            {
                P.Factor = 0;
                Q.Factor = 0;
            }
            timer.Logcreate("Rotación Simple a la Izquierda");
            
        }

        protected void RotacionDDerecha(Nodo<T> nodo)
        {
            var timer = new LogFile();
            var RootParent = nodo.Padre;
            var P = nodo as Nodo<T>;
            var Q = P.Izquierdo;
            var R = Q.Derecho;
            var B = R.Izquierdo;
            var C = R.Derecho;

            if(RootParent != null)
            {
                if(RootParent.Derecho == P)
                {
                    RootParent.Derecho = R;
                }
                else
                {
                    RootParent.Izquierdo = R;
                }
            } else
            {
                root = R;
                root.Padre = null;
            }

            Q.Derecho = B;
            P.Izquierdo = C;
            R.Izquierdo = Q;
            R.Derecho = P;

            R.Padre = RootParent;
            P.Padre = Q.Padre = R;

            if (B != null)
                B.Padre = Q;
            if(C != null)

            {
                C.Padre = P;
            }

            switch (R.Factor)
            {
                case -1:
                    {
                        Q.Factor = 0;
                        P.Factor = 1;
                    }
                    break;
                case 0:
                    {
                        Q.Factor = 0;
                        P.Factor = 0;
                      
                    }break;
                case 1:
                    {
                        Q.Factor = -1;
                        P.Factor = 0;
                    }break;
            }
            R.Factor = 0;
            timer.Logcreate("Rotación Doble a la derecha");

        }

        protected void RotacionDIzquierda(Nodo<T> nodo)
        {
            var timer = new LogFile();
            var RParent = nodo.Padre;
            var P = nodo;
            var Q = nodo.Derecho;
            var R = Q.Izquierdo;
            var B = R.Izquierdo;
            var C = R.Derecho;

            if(RParent != null)
            {
                if(RParent.Derecho == P)
                {
                    RParent.Derecho = R;
                }
                else
                {
                    RParent.Izquierdo = R;
                }
            }
            P.Derecho = B;
            Q.Izquierdo = C;
            R.Izquierdo = P;
            R.Derecho = Q;

            R.Padre = RParent;
            P.Padre = Q.Padre = R;

            if(B != null)
            {
                B.Padre = P;
            }
            if (C != null)
                C.Padre = Q;
            switch (R.Factor)
            {
                case -1:
                    {
                        P.Factor= 0;
                        Q.Factor = 1;
                    }
                    break;

                case 0:
                    {
                        P.Factor = 0;
                        Q.Factor = 0;
                    }
                    break;

                case 1:
                    {
                        P.Factor = -1;
                        Q.Factor = 0;
                    }
                    break;
            }

            timer.Logcreate("Rotación Doble a la Izquierda");


        }
        private void Balancear(Nodo<T> node, bool isnew, bool isleft)
        {
            var timer = new LogFile();
            var exit = false;

            while ((node != null) && !exit)
            {
                var didRotate = false;

                if (isnew)
                {
                    if (isleft)
                    {
                        node.Factor--;
                    }
                    else
                    {
                        node.Factor++;
                    }
                }
                else
                {
                    if (node.Factor == 0)

                        exit = true;
                    timer.Logcreate("Balanceo");

                    if (isleft)

                        node.Factor++;

                    else

                        node.Factor--;


                }
                if (node.Factor == 0)
                {
                    exit = true;
                    timer.Logcreate("Balanceo");
                }

                else if (node.Factor == -2)
                {
                    if (node.Izquierdo.Factor == 1)
                    {
                        RotacionDDerecha(node);
                        didRotate = true;
                    }
                    else
                    {
                        RotaciónDerecha(node);
                        didRotate = true;
                    }
                    exit = true;
                    timer.Logcreate("Balanceo");
                }
                else if (node.Factor == 2)
                {
                    if (node.Derecho.Factor == -1)
                    {
                        RotacionDIzquierda(node);
                        didRotate = true;
                    }
                    else
                    {
                        RotaciónIzquierda(node);
                        didRotate = true;
                    }
                    exit = true;
                    timer.Logcreate("Balanceo");
                }
                if ((didRotate) && (node.Padre != null) && (!isnew))
                    node = node.Padre;

                if (node.Padre != null)
                {
                    if (node.Padre.Derecho == node)
                    {
                        isleft = false;
                    }
                    else
                    {
                        isleft = true;
                    }
                    if ((!isnew) && (node.Factor == 0))
                    {
                        exit = false;
                    }
                }
                node = node.Padre;

            }
        }

        #endregion

    }
}
