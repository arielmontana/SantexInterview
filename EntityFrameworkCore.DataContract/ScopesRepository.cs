using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.DataContract
{

    public class ScopesRepository : IDisposable
    {
        public event EventHandler OnDispose;
        private IDisposable dbContextScope { get; set; }
        public ScopesRepository(IDisposable context)
        {
            dbContextScope = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContextScope != null) dbContextScope.Dispose();
                OnDispose(this, new EventArgs());
            }
        }
    }
}
