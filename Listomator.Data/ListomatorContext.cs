using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Listomator.Data.Model
{
    public class ListomatorContext : DbContext
    {
        public DbSet<ToDoGroup> ToDoGroups { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        private static readonly string DatabasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "Listomator.db3");

        protected override async void OnConfiguring(DbContextOptionsBuilder options)
        {
            // use this for creating migrations
            string dbPath;
            if (DeviceInfo.Platform == DevicePlatform.Unknown)
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Listomator.db3");
            else
                dbPath = Path.Combine(FileSystem.AppDataDirectory, "Listomator.db3");
            
            
            options.UseSqlite($"DataSource={dbPath}");

            //await Database.EnsureCreatedAsync();
            //await Database.MigrateAsync();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private ReaderWriterLockSlim readwriteLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public override int SaveChanges()
        {
            bool isBreaked = false;
            int ret = 0;

            try
            {
                readwriteLock.EnterWriteLock();
                if (readwriteLock.WaitingWriteCount > 0)
                    isBreaked = true;
                else
                {
                    ret = base.SaveChanges();
                }
            }
            catch (SqliteException ex)
            {
                if (ex.Message == "SQLite Error 5: 'database is locked'.")
                    isBreaked = true;
            }
            catch (Exception ex)
            {
                var a = ex;
            }
            finally
            {
                readwriteLock.ExitWriteLock();
            }

            if (isBreaked)
            {
                Thread.Sleep(10);
                ret = base.SaveChanges();
            }

            return ret;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            bool isBreaked = false;
            int ret = 0;

            try
            {
                readwriteLock.EnterWriteLock();
                if (readwriteLock.WaitingWriteCount > 0)
                    isBreaked = true;
                else
                {
                    ret = await base.SaveChangesAsync(cancellationToken);
                }
            }
            catch (SqliteException ex)
            {
                if (ex.Message == "SQLite Error 5: 'database is locked'.")
                    isBreaked = true;
            }
            catch (Exception ex)
            {
                var a = ex;
            }
            finally
            {
                readwriteLock.ExitWriteLock();
            }

            if (isBreaked)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    Thread.Sleep(10);
                    ret = await base.SaveChangesAsync(cancellationToken);
                }
            }

            return ret;
        }
    }
}
