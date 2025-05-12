using Microsoft.EntityFrameworkCore;
using SistemaBancario.Models;

namespace SistemaBancario.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ContaBancaria> ContasBancarias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Cartao> Cartaos { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento Cliente → ContaBancaria (um-para-muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.ContasBancarias)
                .WithOne(cb => cb.Cliente)
                .HasForeignKey(cb => cb.ClienteId);

            // Relacionamento ContaBancaria → Transacoes (um-para-muitos)
            modelBuilder.Entity<ContaBancaria>()
                .HasMany(cb => cb.TransacoesOrigem)
                .WithOne(t => t.ContaOrigem)
                .HasForeignKey(t => t.ContaOrigemId)
                .OnDelete(DeleteBehavior.Restrict); // Previne deleção em cascata das contas

            modelBuilder.Entity<ContaBancaria>()
                .HasMany(cb => cb.TransacoesDestino)
                .WithOne(t => t.ContaDestino)
                .HasForeignKey(t => t.ContaDestinoId)
                .OnDelete(DeleteBehavior.Restrict); // Previne deleção em cascata das contas

            // Relacionamento Cliente → Cartao (um-para-muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Cartoes)
                .WithOne(ca => ca.Cliente)
                .HasForeignKey(ca => ca.ClienteId);

            // Relacionamento Cliente → Emprestimo (um-para-muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Emprestimos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.ClienteId);

            // Relacionamento Emprestimo → ContaBancaria (um-para-um) (opcional)
            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.ContaBancaria)         
                .WithMany(cb => cb.Emprestimos)       
                .HasForeignKey(e => e.ContaBancariaId) 
                .OnDelete(DeleteBehavior.SetNull);      
        }
    }
}
