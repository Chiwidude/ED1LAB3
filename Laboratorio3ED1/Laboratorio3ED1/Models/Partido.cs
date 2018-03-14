using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio3ED1.Models
{
    public class Partido 
    {

        private DateTime fecha;
        private string Equipo1, Equipo2, grupo, estadio;
        private int Npartido;
        

        /// <summary>
        /// Constructor Clase Partido 
        /// </summary>
        /// <param name="fecha"></param>Fecha en la que se realizará el partido
        /// <param name="equipo1"></param> Equipo 1
        /// <param name="equipo2"></param>Equipo 2
        /// <param name="grupo"></param> grupo al que pertenecen
        /// <param name="estadio"></param>Estadio donde se realizará el encuentro
        /// <param name="npartido"></param>Número de partido 
        public Partido(DateTime fecha, string equipo1, string equipo2, string grupo, string estadio, int npartido)
        {
            this.Fecha = fecha;
            Equipo1_ = equipo1;
            Equipo2_ = equipo2;
            this.Grupo = grupo;
            this.Estadio = estadio;
            Npartido_ = npartido;
            
        }

        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Equipo1_ { get => Equipo1; set => Equipo1 = value; }
        public string Equipo2_ { get => Equipo2; set => Equipo2 = value; }
        public string Grupo { get => grupo; set => grupo = value; }
        public string Estadio { get => estadio; set => estadio = value; }
        public int Npartido_ { get => Npartido; set => Npartido = value; }

    }
}