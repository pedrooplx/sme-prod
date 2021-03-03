﻿using sme.business.Models.Enums;
using System.Collections.Generic;

namespace sme.business.Models
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        //EF Relations
        public IEnumerable<Produto> Produto { get; set; }
    }
}
