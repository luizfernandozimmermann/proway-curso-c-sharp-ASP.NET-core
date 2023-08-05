﻿using LojaRepositorios.repositorios;
using LojaRepositorios.entidades;
using LojaServicos.Dtos.Clientes;

namespace LojaServicos.servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public void Cadastrar(ClienteCadastroDto dto)
        {
            var cliente = ConstruirCliente(dto);
            _clienteRepositorio.Cadastrar(cliente);
        }

        public void Apagar(int id)
        {
            _clienteRepositorio.Apagar(id);
        }

        public List<ClienteIndexDto> ObterTodos(string? pesquisa)
        {
            var clientes = _clienteRepositorio.ObterTodos(pesquisa);

            var clientesDto = ConstruirClientesDto(clientes);

            return clientesDto;
        }

        private List<ClienteIndexDto> ConstruirClientesDto(List<Cliente> clientes)
        {
            var dtos = new List<ClienteIndexDto>();

            foreach (var cliente in clientes)
            {
                var dto = new ClienteIndexDto 
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    Endereco = $"{cliente.Endereco.Estado} - {cliente.Endereco.Cidade}"
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public Cliente? ObterPorId(int id)
        {
            return _clienteRepositorio.ObterPorId(id);
        }

        public void Editar(Cliente cliente)
        {
            _clienteRepositorio.Editar(cliente);
        }

        private Cliente ConstruirCliente(ClienteCadastroDto dto)
        {
            return new Cliente
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                Cpf = dto.Cpf,
                Endereco = new Endereco
                {
                    Estado = dto.Estado,
                    Cep = dto.Cep,
                    Cidade = dto.Cidade,
                    Logradouro = dto.Logradouro,
                    Bairro = dto.Bairro,
                    Numero = dto.Numero,
                    Complemento = dto.Complemento
                }
            };
        }
    }
}
