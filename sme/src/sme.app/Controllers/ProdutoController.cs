using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sme.app.ViewModels;
using sme.business.Interfaces;
using sme.business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace sme.app.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository,
                                 IFornecedorRepository fornecedorRepository,
                                 IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View(await PopularFornecedores(new ProdutoViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
            {
                produtoViewModel = await PopularFornecedores(new ProdutoViewModel());
                return View(produtoViewModel);
            }

            var prefixo = Guid.NewGuid() + "_" + produtoViewModel.ImagemUpload.FileName;
            if (!await UploadImagem(produtoViewModel.ImagemUpload, prefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = prefixo;
            await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);
            produtoViewModel.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                var prefixo = Guid.NewGuid() + "_" + produtoViewModel.ImagemUpload.FileName;
                if (!await UploadImagem(produtoViewModel.ImagemUpload, prefixo))
                {
                    return View(produtoViewModel);
                }

                DeleteImagem(produtoAtualizacao.Imagem);

                produtoAtualizacao.Imagem = prefixo;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            await _produtoRepository.Remover(id);

            return RedirectToAction("Index");
        }
        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

            return produto;
        }
        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }
        private async Task<bool> UploadImagem(IFormFile img, string prefixo)
        {
            if (img.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", prefixo);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome, altere e tente novamente!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await img.CopyToAsync(stream);
            }

            return true;
        }

        private bool DeleteImagem(string prefixo)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", prefixo);
            System.IO.File.Delete(path);
            return System.IO.File.Exists(path) ? false : true;
        }
    }
}
