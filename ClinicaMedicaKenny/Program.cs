using Bogus;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedicaKenny
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ConsoleTarget
            {
                Name = "console",
                Layout = "${message}\n",
            };
            var ft = new FileTarget
            {
                Name = "FileLogger",
                FileName = "C:\\Temp\\NLogOut.txt",
                Layout = "${message}\n",
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget, "*");
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, ft, "*");
            LogManager.Configuration = config;
            System.Data.Entity.Infrastructure.Interception.DbInterception.Add(new NLogCommandInterceptor());
            var db = new ClinicaMedicaEntities1();
            //db.Database.Log = s => Console.WriteLine(s);
            var sw = new Stopwatch();
            sw.Start();
#if false
            Logger.Debug("Limpando tabelas");
            db.MEDICO_ESPECIALIDADE.ToList().ForEach(p => db.MEDICO_ESPECIALIDADE.Remove(p));
            db.CONSULTA.ToList().ForEach(p => db.CONSULTA.Remove(p));
            db.TELEFONE_MEDICO.ToList().ForEach(p => db.TELEFONE_MEDICO.Remove(p));
            db.PACIENTE.ToList().ForEach(p => db.PACIENTE.Remove(p));
            db.ESPECIALIDADE.ToList().ForEach(p => db.ESPECIALIDADE.Remove(p));
            db.TIPO_SANGUE.ToList().ForEach(p => db.TIPO_SANGUE.Remove(p));
            db.MEDICO.ToList().ForEach(p => db.MEDICO.Remove(p));
            db.SaveChanges();
            Logger.Debug("----------------");
            Logger.Debug("Listando Medicos");
            var medicos = db.MEDICO.ToList();
            Logger.Debug("----------------");
            var mf = new Faker<MEDICO>("pt_BR")
                .RuleFor(p => p.NM_NOME, p => p.Name.FullName())
                .RuleFor(p => p.DT_ADIMISSAO, p => p.Date.Past(19))
                .RuleFor(p=>p.NR_CRM,p=>p.Address.StateAbbr() + '-' + p.Random.Int(1).ToString())
                .GenerateLazy(10)
                .ToList();

            mf.ForEach(p => db.MEDICO.Add(p));
            Logger.Debug("Inserindo Medicos");
            db.SaveChanges();
            Logger.Debug("----------------");
            var ms = db.MEDICO.ToList();
            Logger.Debug("Inserindo telefones de médicos");
            ms.ForEach(m => 
            {
                //m.TELEFONE_MEDICO = new List<TELEFONE_MEDICO>();
                new Faker<TELEFONE_MEDICO>("pt_BR")
                    .RuleFor(p => p.NR_TELEFONE, p => p.Phone.PhoneNumber().Replace("+", string.Empty).Replace(" ", string.Empty).Replace("55(", "("))
                    .GenerateLazy(3)
                    .ToList()
                    .ForEach(p =>
                    {
                        m.TELEFONE_MEDICO.Add(p);
                        db.SaveChanges();
                    });
            });
            Logger.Debug("----------------");
            Logger.Debug("Inserindo especialidades médicas");
            var esp = new List<string>();
            esp.Add("Gastroenterologia");
            esp.Add("Oftalmologia");
            esp.Add("Imunologia");
            esp.Add("Cardiologia");
            esp.Add("Infectologia");
            esp.ForEach(p =>
            {
                db.ESPECIALIDADE.Add(new ESPECIALIDADE 
                {
                    NM_ESPECIALIDADE = p
                });
                db.SaveChanges();
            });
            Logger.Debug("----------------");
            Logger.Debug("Inserindo medico-especialidade");
            ms = db.MEDICO.ToList();
            ms.ForEach(m =>
            {
                db.ESPECIALIDADE.ToList().ForEach(e => 
                {
                    db.MEDICO_ESPECIALIDADE.Add(new MEDICO_ESPECIALIDADE
                    {
                        ID_ESPECIALIDADE = e.ID_ESPECIALIDADE,
                        NR_CRM = m.NR_CRM
                    });
                });
            });
            db.SaveChanges();
            Logger.Debug("----------------");
            Logger.Debug("Inserindo paciente");
            new Faker<PACIENTE>("pt_BR")
                .RuleFor(p => p.NM_NOME, p => p.Name.FullName())
                .RuleFor(p => p.DS_ENDERECO, p => p.Address.FullAddress())
                .RuleFor(p => p.NR_TELEFONE, p => p.Phone.PhoneNumber().Replace("+", string.Empty).Replace(" ", string.Empty).Replace("55(", "("))
                .RuleFor(p => p.DS_CODIGO, p => p.Random.Int(1).ToString())
                .GenerateLazy(10)
                .ToList()
                .ForEach(p => db.PACIENTE.Add(p));
            db.SaveChanges();
            Logger.Debug("----------------");
            Logger.Debug("Inserindo tipo de sangue");
            var tp_sng = new List<string>();
            tp_sng.Add("A");
            tp_sng.Add("B");
            tp_sng.Add("AB");
            tp_sng.Add("O");
            var fh = new List<string>();
            fh.Add("+");
            fh.Add("-");
            var r = new Random();
            tp_sng.ForEach(t =>
            {
                fh.ForEach(h =>
                {
                    db.TIPO_SANGUE.Add(new TIPO_SANGUE
                    {
                        DS_RH = h,
                        DS_TIPO = t
                    });
                });
            });
            db.SaveChanges();
            Logger.Debug("----------------");
            Logger.Debug("Inserindo tipo de sangue no paciente");
            var tp_sngs = db.TIPO_SANGUE.ToList();
            db.PACIENTE
                .ToList()
                .ForEach(p =>
                {
                    p.ID_TIPO_SANGUE = tp_sngs[r.Next(0, tp_sngs.Count - 1)].ID_TIPO_SANGUE;
                });
            db.SaveChanges();
            Logger.Debug("----------------");
            Logger.Debug("Inserindo consulta");
            var mds = db.MEDICO.ToList();
            db.PACIENTE
                .ToList()
                .ForEach(p =>
                {
                    db.CONSULTA.Add(new CONSULTA
                    {
                        DT_MARCADO = new Faker().Date.Past(),
                        NR_CRM = mds[r.Next(0, mds.Count-1)].NR_CRM,
                        ID_PACIENTE = p.ID_PACIENTE
                    });
                });
            db.SaveChanges();
            Logger.Debug("----------------");
#endif
            Logger.Debug("Consulta com medico completa");
            var q = db.CONSULTA.Select(c => new { DATA_MARCADO = c.DT_MARCADO, NOME_PACIENTE = c.PACIENTE.NM_NOME, PACIENTE_TELEFONE = c.PACIENTE.NR_TELEFONE, PACIENTE_COD = c.PACIENTE.DS_CODIGO, TIPO_SANGUE = c.PACIENTE.TIPO_SANGUE.DS_TIPO, FATOR_SANGUE = c.PACIENTE.TIPO_SANGUE.DS_RH, ENDERECO_PAC = c.PACIENTE.DS_ENDERECO, MED_NOME = c.MEDICO.NM_NOME, MED_CRM = c.MEDICO.NR_CRM, MED_DT_AT = c.MEDICO.DT_ADIMISSAO }).ToList();
            Logger.Debug("----------------");
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;

            Console.WriteLine("Script levou: {0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            Console.ReadKey();
        }
    }
}
