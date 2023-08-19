﻿using FluentValidation;
using LojaApi.Controllers;

namespace LojaApi.Validators
{
    public class ProdutoValidator : AbstractValidator<ProdutoCreateModel>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("{PropertyName} deve pode ser nulo")
                .NotEmpty()
                .WithMessage("{PropertyName} não deve ser vazio")
                .MinimumLength(3)
                .WithMessage("{PropertyName} deve conter no mínimo {MinLenght} caracteres")
                .MaximumLength(40)
                .WithMessage("{PropertyName} deve conter no máximo {MaxLenght} caracteres");

            RuleFor(x => x.Quantidade)
                .GreaterThan(0)
                .WithMessage("Quantidade deve ser maior que 0");

            RuleFor(x => x.PrecoUnitario) 
                .GreaterThan(0)
                .WithMessage("Preço unitário deve ser maior que 0");
        }
    }
}