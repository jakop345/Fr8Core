﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.States.Templates;

namespace Data.Entities
{
    public class TerminalDO : BaseDO, ITerminalDO
    {
        public TerminalDO()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Version { get; set; }

        [ForeignKey("TerminalStatusTemplate")]
        public int TerminalStatus { get; set; }
        public _TerminalStatusTemplate TerminalStatusTemplate { get; set; }

        // TODO: remove this, DO-1397
        // public bool RequiresAuthentication { get; set; }

        //public string BaseEndPoint { get; set; }

        public string Endpoint { get; set; }
    }
}
