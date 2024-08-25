using Microsoft.EntityFrameworkCore;
using Minesweeper;

public class MinesweeperContext : DbContext
{
    private string _dbPath;

    public DbSet<Score> Scores { get; set; }

    public MinesweeperContext() : base()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
            _dbPath = System.IO.Path.Join(path, "minesweeper.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite($"Data Source={_dbPath}");
}
