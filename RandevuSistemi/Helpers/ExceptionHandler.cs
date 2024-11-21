// Purpose: Custom exception handler class for logging exceptions.
using System.Data.SqlClient; // SqlException
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices; // CallerMemberName icin gerekli
using System.Diagnostics; // StackTrace for callsite
using Newtonsoft.Json;

namespace RandevuSistemi.Helpers
{
    public class CustomExceptionLogger : ControllerBase
    {
        private readonly ILogger<CustomExceptionLogger> _logger;
        private readonly bool isDevelopment = false;
        /// <summary>
        /// gelistirme ortaminda loglama islemini kapatmak icin isDevelopment degiskenini true yapabilirsiniz.   @hakan
        /// </summary>

        public CustomExceptionLogger(ILogger<CustomExceptionLogger> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
        }


        // Method to get the full callsite information
        private string GetFullMethodPath([CallerMemberName] string methodName = "")
        {
            var stackTrace = new StackTrace();
            // Getting the frame of the method that called LogException (skipping the first frame)
            var frame = stackTrace.GetFrame(2);
            var method = frame?.GetMethod();
            var fullMethodPath = $"{method?.DeclaringType?.FullName}.{method?.Name}";
            return fullMethodPath;
        }

        public IActionResult LogException(Exception ex, [CallerMemberName] string methodName = "")
        {
            if (isDevelopment)
            {
                return Ok("Development environment, no logging."); // Gelistirme ortam�nda loglama yapma
            }

            string fullMethodPath = GetFullMethodPath(methodName);
            if (ex is ArgumentNullException)
            {
                _logger.LogError("{0} Error: ArgumentNullException - Gerekli bir parametre null olarak gecildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new BadRequestObjectResult("Gerekli bir parametre eksik."); // 400 Bad Request
            }
            else if (ex is InvalidOperationException)
            {
                _logger.LogError("{0} Error: InvalidOperationException - Gecersiz bir islem yapildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new BadRequestObjectResult("Gecersiz bir islem yapildi."); // 400 Bad Request
            }
            else if (ex is UnauthorizedAccessException)
            {
                _logger.LogError("{0} Error: UnauthorizedAccessException - Yetkisiz erisim hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new UnauthorizedResult(); // 401 Unauthorized
            }
            else if (ex is NullReferenceException)
            {
                _logger.LogError("{0} Error: NullReferenceException - Null referansi kullanildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new NotFoundObjectResult("Istenilen veri bulunamadi."); // 404 Not Found
            }
            else if (ex is IndexOutOfRangeException)
            {
                _logger.LogError("{0} Error: IndexOutOfRangeException - Dizi indeks araligi disinda. Detaylar: {1}", fullMethodPath, ex.Message);
                return new BadRequestObjectResult("Dizi indeksi gecersiz."); // 400 Bad Request
            }
            else if (ex is FileNotFoundException)
            {
                _logger.LogError("{0} Error: FileNotFoundException - Dosya bulunamadi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new NotFoundObjectResult("Dosya bulunamadi."); // 404 Not Found
            }
            else if (ex is FormatException)
            {
                _logger.LogError("{0} Error: FormatException - Yanlis formatta veri. Detaylar: {1}", fullMethodPath, ex.Message);
                return new BadRequestObjectResult("Yanlis veri formati."); // 400 Bad Request
            }
            else if (ex is DivideByZeroException)
            {
                _logger.LogError("{0} Error: DivideByZeroException - Sifira bolme hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new BadRequestObjectResult("Sifira bolme hatasi."); // 400 Bad Request
            }
            else if (ex is OutOfMemoryException)
            {
                _logger.LogError("{0} Error: OutOfMemoryException - Yetersiz bellek. Detaylar: {1}", fullMethodPath, ex.Message);
                return new StatusCodeResult(500); // 500 Internal Server Error
            }
            else if (ex is IOException)
            {
                _logger.LogError("{0} Error: IOException - Giris/Cikis hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new StatusCodeResult(500); // 500 Internal Server Error
            }
            else if (ex is SqlException)
            {
                _logger.LogError("{0} Error: SqlException - Veritabani hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new StatusCodeResult(500); // 500 Internal Server Error
            }
            else if (ex is TaskCanceledException)
            {
                _logger.LogError("{0} Error: TaskCanceledException - Gorev iptal edildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return new StatusCodeResult(408); // 408 Request Timeout
            }
            else
            {
                _logger.LogError("{0} Error: Exception - Bilinmeyen hata: {1}. Detaylar: {2}", fullMethodPath, ex.GetType().Name, ex.Message);
                return new StatusCodeResult(500); // 500 Internal Server Error
            }
        }

        public ActionResult LogExceptionActionResult(Exception ex, [CallerMemberName] string methodName = "")
        {
            if (isDevelopment)
            {
                return Ok("Development environment, no logging."); // Geli�tirme ortam�nda loglama yapma
            }

            string fullMethodPath = GetFullMethodPath(methodName);
            if (ex is ArgumentNullException)
            {
                _logger.LogError("{0} Error: ArgumentNullException - Gerekli bir parametre null olarak gecildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return BadRequest("Gerekli bir parametre eksik."); // 400 Bad Request
            }
            else if (ex is InvalidOperationException)
            {
                _logger.LogError("{0} Error: InvalidOperationException - Gecersiz bir islem yapildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return BadRequest("Gecersiz bir islem yapildi."); // 400 Bad Request
            }
            else if (ex is UnauthorizedAccessException)
            {
                _logger.LogError("{0} Error: UnauthorizedAccessException - Yetkisiz erisim hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return Unauthorized(); // 401 Unauthorized
            }
            else if (ex is NullReferenceException)
            {
                _logger.LogError("{0} Error: NullReferenceException - Null referansi kullanildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return NotFound("Istenilen veri bulunamadi."); // 404 Not Found
            }
            else if (ex is IndexOutOfRangeException)
            {
                _logger.LogError("{0} Error: IndexOutOfRangeException - Dizi indeks araligi disinda. Detaylar: {1}", fullMethodPath, ex.Message);
                return BadRequest("Dizi indeksi gecersiz."); // 400 Bad Request
            }
            else if (ex is FileNotFoundException)
            {
                _logger.LogError("{0} Error: FileNotFoundException - Dosya bulunamadi. Detaylar: {1}", fullMethodPath, ex.Message);
                return NotFound("Dosya bulunamadi."); // 404 Not Found
            }
            else if (ex is FormatException)
            {
                _logger.LogError("{0} Error: FormatException - Yanlis formatta veri. Detaylar: {1}", fullMethodPath, ex.Message);
                return BadRequest("Yanlis veri formati."); // 400 Bad Request
            }
            else if (ex is DivideByZeroException)
            {
                _logger.LogError("{0} Error: DivideByZeroException - Sifira bolme hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return BadRequest("Sifira bolme hatasi."); // 400 Bad Request
            }
            else if (ex is OutOfMemoryException)
            {
                _logger.LogError("{0} Error: OutOfMemoryException - Yetersiz bellek. Detaylar: {1}", fullMethodPath, ex.Message);
                return StatusCode(500); // 500 Internal Server Error
            }
            else if (ex is IOException)
            {
                _logger.LogError("{0} Error: IOException - Giris/Cikis hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return StatusCode(500); // 500 Internal Server Error
            }
            else if (ex is SqlException)
            {
                _logger.LogError("{0} Error: SqlException - Veritabani hatasi. Detaylar: {1}", fullMethodPath, ex.Message);
                return StatusCode(500); // 500 Internal Server Error
            }
            else if (ex is TaskCanceledException)
            {
                _logger.LogError("{0} Error: TaskCanceledException - Gorev iptal edildi. Detaylar: {1}", fullMethodPath, ex.Message);
                return StatusCode(408); // 408 Request Timeout
            }
            else
            {
                _logger.LogError("{0} Error: Exception - Bilinmeyen hata: {1}. Detaylar: {2}", fullMethodPath, ex.GetType().Name, ex.Message);
                return StatusCode(500); // 500 Internal Server Error
            }
        }

        public void LogLogin([CallerMemberName] string methodName = "")
        {
            if (isDevelopment)
            {
                return; // Gelistirme ortaminda loglama yapma
            }

            string fullMethodPath = GetFullMethodPath(methodName);
            _logger.LogDebug("{0} Giris.", fullMethodPath);
        }

        public void LogFail([CallerMemberName] string methodName = "")
        {
            if (isDevelopment)
            {
                return; // Gelistirme ortaminda loglama yapma
            }

            string fullMethodPath = GetFullMethodPath(methodName);
            _logger.LogDebug("{0} Fail.", fullMethodPath);
        }

        public void LogSuccess([CallerMemberName] string methodName = "")
        {
            if (isDevelopment)
            {
                return; // Gelistirme ortaminda loglama yapma
            }

            string fullMethodPath = GetFullMethodPath(methodName);
            _logger.LogDebug("{0} Success.", fullMethodPath);
        }

        public void LogParameterInformation(object? args = null, [CallerMemberName] string methodName = "")
        {
            if (isDevelopment)
            {
                return; // Gelistirme ortaminda loglama yapma
            }

            string fullMethodPath = GetFullMethodPath(methodName);

            if (args != null)
            {
                _logger.LogInformation("{0} Gelen parametre: {1}", fullMethodPath, JsonConvert.SerializeObject(args));
            }
            else
            {
                _logger.LogInformation("{0} Fonksiyon Parametresiz calisti veya parametre boyutu boyutu buyuk oldugu icin yazdirilmadi.", fullMethodPath);
            }
        }

        public void LogReturnParameterInformation(object? args = null, [CallerMemberName] string methodName = "")
        {
            if (isDevelopment)
            {
                return; // Gelistirme ortaminda loglama yapma
            }

            string fullMethodPath = GetFullMethodPath(methodName);

            if (args != null)
            {
                _logger.LogInformation("{0} Donen parametre: {1}", fullMethodPath, JsonConvert.SerializeObject(args));
            }
            else
            {
                _logger.LogInformation("{0} Fonksiyon donus degeri olmadigi icin veya donus degeri buyuk oldugu icin yazdirilmadi.", fullMethodPath);
            }
        }

    }
}