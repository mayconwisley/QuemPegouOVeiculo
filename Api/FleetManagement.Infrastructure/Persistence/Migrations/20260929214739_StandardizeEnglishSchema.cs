using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StandardizeEnglishSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER SCHEMA "cadastros" RENAME TO "registrations";
                ALTER SCHEMA "operacoes" RENAME TO "operations";
                ALTER TABLE "registrations"."motoristas" RENAME TO "drivers";
                ALTER TABLE "registrations"."veiculos" RENAME TO "vehicles";
                ALTER TABLE "operations"."vencimentos_cnh" RENAME TO "license_expirations";
                ALTER TABLE "operations"."abastecimentos" RENAME TO "refuelings";
                ALTER TABLE "operations"."manutencoes" RENAME TO "maintenance_records";
                ALTER TABLE "operations"."movimentacoes" RENAME TO "vehicle_movements";
                ALTER TABLE "operations"."multas" RENAME TO "fines";
                ALTER TABLE "operations"."status_veiculos" RENAME TO "vehicle_statuses";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "nome" TO "name";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "cnh" TO "license_number";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "vencimento_cnh" TO "license_expiration";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "categoria_cnh" TO "license_category";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "ativo" TO "active";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "placa" TO "plate";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "modelo" TO "model";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "chassi" TO "chassis";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "ativo" TO "active";
                ALTER TABLE "operations"."license_expirations" RENAME COLUMN "motorista_id" TO "driver_id";
                ALTER TABLE "operations"."license_expirations" RENAME COLUMN "data" TO "date";
                ALTER TABLE "operations"."license_expirations" RENAME COLUMN "vencido" TO "expired";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "veiculo_id" TO "vehicle_id";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "motorista_id" TO "driver_id";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "quilometragem" TO "mileage";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "data" TO "date";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "valor" TO "amount";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "litros" TO "liters";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "descricao" TO "description";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "veiculo_id" TO "vehicle_id";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "data" TO "date";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "valor" TO "amount";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "descricao" TO "description";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "veiculo_id" TO "vehicle_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "motorista_id" TO "driver_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "saida_utc" TO "departure_utc";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "chegada_utc" TO "arrival_utc";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "km_inicial" TO "initial_mileage";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "km_final" TO "final_mileage";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "descricao" TO "description";
                ALTER TABLE "operations"."fines" RENAME COLUMN "veiculo_id" TO "vehicle_id";
                ALTER TABLE "operations"."fines" RENAME COLUMN "motorista_id" TO "driver_id";
                ALTER TABLE "operations"."fines" RENAME COLUMN "data" TO "date";
                ALTER TABLE "operations"."fines" RENAME COLUMN "valor" TO "amount";
                ALTER TABLE "operations"."fines" RENAME COLUMN "pontos" TO "points";
                ALTER TABLE "operations"."fines" RENAME COLUMN "descricao" TO "description";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "veiculo_id" TO "vehicle_id";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "inicio_utc" TO "start_utc";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "fim_utc" TO "end_utc";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "descricao" TO "description";
                ALTER TABLE "registrations"."drivers" RENAME CONSTRAINT "PK_motoristas" TO "PK_drivers";
                ALTER TABLE "registrations"."vehicles" RENAME CONSTRAINT "PK_veiculos" TO "PK_vehicles";
                ALTER TABLE "operations"."license_expirations" RENAME CONSTRAINT "PK_vencimentos_cnh" TO "PK_license_expirations";
                ALTER TABLE "operations"."license_expirations" RENAME CONSTRAINT "FK_vencimentos_cnh_motoristas_motorista_id" TO "FK_license_expirations_drivers_driver_id";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "PK_abastecimentos" TO "PK_refuelings";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "ck_abastecimentos_valores" TO "ck_refuelings_values";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "FK_abastecimentos_motoristas_motorista_id" TO "FK_refuelings_drivers_driver_id";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "FK_abastecimentos_veiculos_veiculo_id" TO "FK_refuelings_vehicles_vehicle_id";
                ALTER TABLE "operations"."maintenance_records" RENAME CONSTRAINT "PK_manutencoes" TO "PK_maintenance_records";
                ALTER TABLE "operations"."maintenance_records" RENAME CONSTRAINT "ck_manutencoes_valor" TO "ck_maintenance_records_amount";
                ALTER TABLE "operations"."maintenance_records" RENAME CONSTRAINT "FK_manutencoes_veiculos_veiculo_id" TO "FK_maintenance_records_vehicles_vehicle_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "PK_movimentacoes" TO "PK_vehicle_movements";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "ck_movimentacoes_chegada" TO "ck_vehicle_movements_arrival";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "ck_movimentacoes_km" TO "ck_vehicle_movements_mileage";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "FK_movimentacoes_motoristas_motorista_id" TO "FK_vehicle_movements_drivers_driver_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "FK_movimentacoes_veiculos_veiculo_id" TO "FK_vehicle_movements_vehicles_vehicle_id";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "PK_multas" TO "PK_fines";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "ck_multas_valores" TO "ck_fines_values";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "FK_multas_motoristas_motorista_id" TO "FK_fines_drivers_driver_id";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "FK_multas_veiculos_veiculo_id" TO "FK_fines_vehicles_vehicle_id";
                ALTER TABLE "operations"."vehicle_statuses" RENAME CONSTRAINT "PK_status_veiculos" TO "PK_vehicle_statuses";
                ALTER TABLE "operations"."vehicle_statuses" RENAME CONSTRAINT "ck_status_veiculos_datas" TO "ck_vehicle_statuses_dates";
                ALTER TABLE "operations"."vehicle_statuses" RENAME CONSTRAINT "FK_status_veiculos_veiculos_veiculo_id" TO "FK_vehicle_statuses_vehicles_vehicle_id";
                ALTER INDEX "registrations"."IX_motoristas_cpf" RENAME TO "IX_drivers_cpf";
                ALTER INDEX "registrations"."IX_veiculos_placa" RENAME TO "IX_vehicles_plate";
                ALTER INDEX "operations"."IX_abastecimentos_motorista_id" RENAME TO "IX_refuelings_driver_id";
                ALTER INDEX "operations"."IX_abastecimentos_veiculo_id_data" RENAME TO "IX_refuelings_vehicle_id_date";
                ALTER INDEX "operations"."IX_manutencoes_veiculo_id_data" RENAME TO "IX_maintenance_records_vehicle_id_date";
                ALTER INDEX "operations"."IX_movimentacoes_motorista_id_saida_utc" RENAME TO "IX_vehicle_movements_driver_id_departure_utc";
                ALTER INDEX "operations"."IX_movimentacoes_veiculo_id" RENAME TO "IX_vehicle_movements_vehicle_id";
                ALTER INDEX "operations"."IX_multas_motorista_id" RENAME TO "IX_fines_driver_id";
                ALTER INDEX "operations"."IX_multas_veiculo_id_data" RENAME TO "IX_fines_vehicle_id_date";
                ALTER INDEX "operations"."IX_status_veiculos_veiculo_id_inicio_utc" RENAME TO "IX_vehicle_statuses_vehicle_id_start_utc";
                ALTER INDEX "operations"."IX_vencimentos_cnh_motorista_id_data" RENAME TO "IX_license_expirations_driver_id_date";
                """);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER INDEX "operations"."IX_license_expirations_driver_id_date" RENAME TO "IX_vencimentos_cnh_motorista_id_data";
                ALTER INDEX "operations"."IX_vehicle_statuses_vehicle_id_start_utc" RENAME TO "IX_status_veiculos_veiculo_id_inicio_utc";
                ALTER INDEX "operations"."IX_fines_vehicle_id_date" RENAME TO "IX_multas_veiculo_id_data";
                ALTER INDEX "operations"."IX_fines_driver_id" RENAME TO "IX_multas_motorista_id";
                ALTER INDEX "operations"."IX_vehicle_movements_vehicle_id" RENAME TO "IX_movimentacoes_veiculo_id";
                ALTER INDEX "operations"."IX_vehicle_movements_driver_id_departure_utc" RENAME TO "IX_movimentacoes_motorista_id_saida_utc";
                ALTER INDEX "operations"."IX_maintenance_records_vehicle_id_date" RENAME TO "IX_manutencoes_veiculo_id_data";
                ALTER INDEX "operations"."IX_refuelings_vehicle_id_date" RENAME TO "IX_abastecimentos_veiculo_id_data";
                ALTER INDEX "operations"."IX_refuelings_driver_id" RENAME TO "IX_abastecimentos_motorista_id";
                ALTER INDEX "registrations"."IX_vehicles_plate" RENAME TO "IX_veiculos_placa";
                ALTER INDEX "registrations"."IX_drivers_cpf" RENAME TO "IX_motoristas_cpf";
                ALTER TABLE "operations"."vehicle_statuses" RENAME CONSTRAINT "FK_vehicle_statuses_vehicles_vehicle_id" TO "FK_status_veiculos_veiculos_veiculo_id";
                ALTER TABLE "operations"."vehicle_statuses" RENAME CONSTRAINT "ck_vehicle_statuses_dates" TO "ck_status_veiculos_datas";
                ALTER TABLE "operations"."vehicle_statuses" RENAME CONSTRAINT "PK_vehicle_statuses" TO "PK_status_veiculos";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "FK_fines_vehicles_vehicle_id" TO "FK_multas_veiculos_veiculo_id";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "FK_fines_drivers_driver_id" TO "FK_multas_motoristas_motorista_id";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "ck_fines_values" TO "ck_multas_valores";
                ALTER TABLE "operations"."fines" RENAME CONSTRAINT "PK_fines" TO "PK_multas";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "FK_vehicle_movements_vehicles_vehicle_id" TO "FK_movimentacoes_veiculos_veiculo_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "FK_vehicle_movements_drivers_driver_id" TO "FK_movimentacoes_motoristas_motorista_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "ck_vehicle_movements_mileage" TO "ck_movimentacoes_km";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "ck_vehicle_movements_arrival" TO "ck_movimentacoes_chegada";
                ALTER TABLE "operations"."vehicle_movements" RENAME CONSTRAINT "PK_vehicle_movements" TO "PK_movimentacoes";
                ALTER TABLE "operations"."maintenance_records" RENAME CONSTRAINT "FK_maintenance_records_vehicles_vehicle_id" TO "FK_manutencoes_veiculos_veiculo_id";
                ALTER TABLE "operations"."maintenance_records" RENAME CONSTRAINT "ck_maintenance_records_amount" TO "ck_manutencoes_valor";
                ALTER TABLE "operations"."maintenance_records" RENAME CONSTRAINT "PK_maintenance_records" TO "PK_manutencoes";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "FK_refuelings_vehicles_vehicle_id" TO "FK_abastecimentos_veiculos_veiculo_id";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "FK_refuelings_drivers_driver_id" TO "FK_abastecimentos_motoristas_motorista_id";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "ck_refuelings_values" TO "ck_abastecimentos_valores";
                ALTER TABLE "operations"."refuelings" RENAME CONSTRAINT "PK_refuelings" TO "PK_abastecimentos";
                ALTER TABLE "operations"."license_expirations" RENAME CONSTRAINT "FK_license_expirations_drivers_driver_id" TO "FK_vencimentos_cnh_motoristas_motorista_id";
                ALTER TABLE "operations"."license_expirations" RENAME CONSTRAINT "PK_license_expirations" TO "PK_vencimentos_cnh";
                ALTER TABLE "registrations"."vehicles" RENAME CONSTRAINT "PK_vehicles" TO "PK_veiculos";
                ALTER TABLE "registrations"."drivers" RENAME CONSTRAINT "PK_drivers" TO "PK_motoristas";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "description" TO "descricao";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "end_utc" TO "fim_utc";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "start_utc" TO "inicio_utc";
                ALTER TABLE "operations"."vehicle_statuses" RENAME COLUMN "vehicle_id" TO "veiculo_id";
                ALTER TABLE "operations"."fines" RENAME COLUMN "description" TO "descricao";
                ALTER TABLE "operations"."fines" RENAME COLUMN "points" TO "pontos";
                ALTER TABLE "operations"."fines" RENAME COLUMN "amount" TO "valor";
                ALTER TABLE "operations"."fines" RENAME COLUMN "date" TO "data";
                ALTER TABLE "operations"."fines" RENAME COLUMN "driver_id" TO "motorista_id";
                ALTER TABLE "operations"."fines" RENAME COLUMN "vehicle_id" TO "veiculo_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "description" TO "descricao";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "final_mileage" TO "km_final";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "initial_mileage" TO "km_inicial";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "arrival_utc" TO "chegada_utc";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "departure_utc" TO "saida_utc";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "driver_id" TO "motorista_id";
                ALTER TABLE "operations"."vehicle_movements" RENAME COLUMN "vehicle_id" TO "veiculo_id";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "description" TO "descricao";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "amount" TO "valor";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "date" TO "data";
                ALTER TABLE "operations"."maintenance_records" RENAME COLUMN "vehicle_id" TO "veiculo_id";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "description" TO "descricao";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "liters" TO "litros";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "amount" TO "valor";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "date" TO "data";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "mileage" TO "quilometragem";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "driver_id" TO "motorista_id";
                ALTER TABLE "operations"."refuelings" RENAME COLUMN "vehicle_id" TO "veiculo_id";
                ALTER TABLE "operations"."license_expirations" RENAME COLUMN "expired" TO "vencido";
                ALTER TABLE "operations"."license_expirations" RENAME COLUMN "date" TO "data";
                ALTER TABLE "operations"."license_expirations" RENAME COLUMN "driver_id" TO "motorista_id";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "active" TO "ativo";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "chassis" TO "chassi";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "model" TO "modelo";
                ALTER TABLE "registrations"."vehicles" RENAME COLUMN "plate" TO "placa";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "active" TO "ativo";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "license_category" TO "categoria_cnh";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "license_expiration" TO "vencimento_cnh";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "license_number" TO "cnh";
                ALTER TABLE "registrations"."drivers" RENAME COLUMN "name" TO "nome";
                ALTER TABLE "operations"."vehicle_statuses" RENAME TO "status_veiculos";
                ALTER TABLE "operations"."fines" RENAME TO "multas";
                ALTER TABLE "operations"."vehicle_movements" RENAME TO "movimentacoes";
                ALTER TABLE "operations"."maintenance_records" RENAME TO "manutencoes";
                ALTER TABLE "operations"."refuelings" RENAME TO "abastecimentos";
                ALTER TABLE "operations"."license_expirations" RENAME TO "vencimentos_cnh";
                ALTER TABLE "registrations"."vehicles" RENAME TO "veiculos";
                ALTER TABLE "registrations"."drivers" RENAME TO "motoristas";
                ALTER SCHEMA "operations" RENAME TO "operacoes";
                ALTER SCHEMA "registrations" RENAME TO "cadastros";
                """);
        }
    }
}
