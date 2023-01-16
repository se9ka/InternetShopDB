using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InternetShopDB
{
    public class Multithreading
    {
       // private bool acquiredLock = false;
        private AutoResetEvent waitHandler = new AutoResetEvent(true);
        private Mutex mutex = new Mutex();
        private Semaphore semaphore = new Semaphore(0, 1);
        private DbContextOptions options;
        public Multithreading(DbContextOptions options)
        {
            this.options = options;
        }
        
        public void LockExample()
        {
            object? locker = new object();

            for (int i = 0; i < 10; i++)
            {
                using (InternetShopContext context = new InternetShopContext(options))
                {
                    Thread myThread = new(() =>
                    {
                        lock (locker)
                        {
                            context.Persons.Add(new Person { Name = "Person " + i,LastName=" " });
                            context.SaveChanges();
                        }
                    });
                    myThread.Start();
                }

            }
        }

        public void MonitorExample()
        {
            object? locker = new object();

            for (int i = 0; i < 10; i++)
            {
                using (InternetShopContext context = new InternetShopContext(options))
                {
                    Thread myThread = new(() =>
                    {
                        bool acquiredLock = false;
                        try
                        {
                            Monitor.Enter(locker, ref acquiredLock);

                            context.Persons.Add(new Person { Name = "Person " + i, LastName = " " });
                            context.SaveChanges();
                        }
                        finally
                        {
                            if (acquiredLock)
                            {
                                Monitor.Exit(locker);
                            }

                        }
                    });
                    myThread.Start();
                }

            }
        }

        public void AutoResetEventExample()
        {
            for (int i = 0; i < 10; i++)
            {
                using (InternetShopContext context = new InternetShopContext(options))
                {
                    Thread myThread = new(() =>
                    {
                        waitHandler.WaitOne();
                        context.Persons.Add(new Person { Name = "Person " + i, LastName = " " });
                        context.SaveChanges();
                        waitHandler.Set();
                    });
                    myThread.Start();
                }

            }
        }

        public void MutexExample()
        {
            for (int i = 0; i < 10; i++)
            {
                using (InternetShopContext context = new InternetShopContext(options))
                {
                    Thread myThread = new(() =>
                    {
                        mutex.WaitOne();
                        context.Persons.Add(new Person { Name = "Person " + i, LastName = " " });
                        context.SaveChanges();
                        mutex.ReleaseMutex();
                    });
                    myThread.Start();
                }

            }
        }

        public void SemaphoreExample()
        {
            for (int i = 0; i < 10; i++)
            {
                using (InternetShopContext context = new InternetShopContext(options))
                {
                    Thread myThread = new(() =>
                    {
                        semaphore.WaitOne();
                        context.Persons.Add(new Person { Name = "Person " + i, LastName = " " });
                        context.SaveChanges();
                        semaphore.Release();
                    });
                    myThread.Start();
                }
            }
        }
    }
}
