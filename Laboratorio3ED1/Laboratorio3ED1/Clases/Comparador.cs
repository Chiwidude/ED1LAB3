using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Laboratorio3ED1.Models;

namespace Laboratorio3ED1.Clases
{
    public class Comparador
    {
        public int CompareByMatchNumber(Partido partido1, Partido partido2)
        {
            return partido1 == null || partido2 == null ? 1 : partido1.Npartido_.CompareTo(partido2.Npartido_);
        }
        public  int CompareByDate(Partido partido1, Partido partido2)
        {
            int comparison;
            if (partido1 == null || partido2 == null)
            {
                return 1;
            }
            else
            {
                comparison = partido1.Fecha.CompareTo(partido2.Fecha);
                if (comparison == 0)
                {
                    comparison = partido1.Equipo1_.CompareTo(partido2.Equipo1_) + partido1.Equipo2_.CompareTo(partido2.Equipo2_);

                    switch (comparison)
                    {
                        case 2:
                            {
                                comparison = 1;
                            }
                            break;

                        case -2:
                            {
                                comparison = -1;
                            }
                            break;
                        case 0:
                            {
                                if (partido1.Equipo1_.CompareTo(partido2.Equipo1_) != 0)
                                {
                                    comparison = partido1.Equipo1_.CompareTo(partido2.Equipo1_);
                                }
                            }
                            break;

                    }
                }
                return comparison;
            }

        }
    }
}