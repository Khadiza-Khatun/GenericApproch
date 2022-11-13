using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Generic_Repository.Model;
using ConsoleGenericRepository.Data_access_Layer;

namespace ConsoleGenericRepository.DataAccessLayer
{
    public class UnitOfWork : IDisposable
    {
        private SalaryDBEntities context = new SalaryDBEntities();

        private GenericRepository<Employee> employeeRepository;

        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {

                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new GenericRepository<Employee>(context);
                }
                return employeeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
