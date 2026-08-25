using AnnouncementManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementManagement.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Announcement> Announcements { get; set; }
	}
}