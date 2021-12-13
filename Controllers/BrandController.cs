using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//Importar el espacio de nombre de donde se encuentra el contexto de la Base de Datos.
using ParcialComputo3.Data;
using ParcialComputo3.Models;

namespace ParcialComputo3.Controllers
{
    ///<summary>
    ///Definiendo que sera un Controlador para API llamado Brand, igual que el controlador.
    ///</summary>
    [ApiController]
    //Definir la Ruta de la API, llamada del mismo nombre que el controller de la API.
    [Route("[controller]")]
    public class BrandController : Controller
    {
        // Creando la referencia al Contexto de la Base de Datos.
        private DatabaseContext _context;

        //Definir el constructor para inicializar el contexto de la Base de Datos.
        public BrandController(DatabaseContext context)
        {
            _context = context;
        }

        ///<summary>
        ///Definicion del Primer Verbo HTTP a utilizar (HttpGet) (Obteniendo las Marcas registradas en la API).
        ///</summary>
        ///<remarks>
        ///Con este verbo HTTP obtendremos el resultado del objeto, los datos de las Marcas registradas en nuestra API.
        ///</remarks>
        ///<response code="250">Registros de Marcas, encontrados correctamente.</response>
        /// <response code="404">NOT FOUND. No se han podido encontrar las Marcas registradas en esta API.</response>
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var brands = await _context.Brands.ToListAsync();
            return brands;
        }

        /// <summary>
        /// Definicion del Verbo HTTP a utilizar (HttpGet) (Obteniendo los registros del ID de Marca solicitada).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP obtendremos el resultado del objeto, los datos de la Marcas solicitada mediante el ID de Marca en nuestra API.
        /// </remarks>
        /// <param name="id">(ID) Identificador de la Marca.</param>
        /// <response code="250">Registro de la Marca, encontrado correctamente.</response>
        /// <response code="404">NOT FOUND. No se ha podido encontrar la Marca solicitada mediante el ID de la Marca en esta API.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandsByID(int id)
        {
            var brands = await _context.Brands.FindAsync(id);
            if(brands==null)
            {
                return NotFound();
            }
            return brands;
        }
    /// <summary>
        /// Definicion del Segundo Verbo HTTP a utilizar (HttpPost) (Creando un nuevo objeto, una nueva Marca en la base de datos de la API).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP crearemos un nuevo objeto osea un nuevo registro de una Marca en nuestra base de Datos de la API.
        /// </remarks>
        /// <param name="brand">Objeto : Marca a crear en los registros de la base de datos de la API.</param>             
        /// <response code="204">Created. Nueva Marca registrada correctamente en la Base de Datos de la API.</response>        
        /// <response code="400">BadRequest. No se ha podido registrar la Marca en la Base de Datos de la API. Formato del registro incorrecto.</response>
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBrandsByID", new{id=brand.idBrand},brand);
        }

        /// <summary>
        /// Definicion del Tercer Verbo HTTP a utilizar (HttpPut) (Actualizando un objeto o registro ya existente en la base de datos de la API, mediante el ID de la Marca).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP actualizaremos un registro osea un objeto, que en este caso es una Marca ya existente en nuestra base de Datos de la API.
        /// El funcionamiento de este verbo es solicitar mediante un ID de la Marca que querramos modificar, por lo cual debemos ingresar el identidicador de esta, para que el verbo devuelva el objeto con los datos que se le habian asignado a este cuando se registro en la BD por primera vez.
        /// </remarks>
        /// <param name="id">(ID) Identificador de la Marca.</param>
        /// <param name="brand">Objeto o Registro a Actualizar.</param>
        /// <response code="204">Update. La Marca se a actualizado correctamente en la Base de Datos de la API.</response>        
        /// <response code="400">BadRequest. No se ha podido actualizar los registros de la Marca en la Base de Datos de la API. Formato del registro incorrecto.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> PutBrand(int id, Brand brand)
        {
            if(id!= brand.idBrand)
            {
                return BadRequest();
            }
            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!BrandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetBrandsByID", new{id=brand.idBrand},brand);
        }

        /// <summary>
        /// Definicion del Cuarto Verbo HTTP a utilizar (HttpDelete) (Eliminando un objeto o registro ya existente de una Marca en la base de datos de la API, mediante el ID de una Marca).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP eliminaremos un registro osea un objeto, que en este caso es una Marca ya existente en nuestra base de Datos de la API.
        /// El funcionamiento de este verbo es solicitar mediante un ID de una Marca que querramos eliminar, por lo cual debemos ingresar el identidicador de esta, para que el verbo busque el objeto con los datos que se le habian asignado a este cuando se registro en la Base de Datos y los elimine de manera satisfactoria.
        /// </remarks>
        /// <param name="id">(ID) Identificador de una Marca.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> DeleteBrand(int id)
        {
            var brands = await _context.Brands.FindAsync(id);
            if(brands==null)
            {
                return NotFound();
            }
            _context.Brands.Remove(brands);
            await _context.SaveChangesAsync();

            return brands;
            
        }

        /// <summary>
        /// Metodo Booleano para saber si existe o no una Marca en los registros de la base de datos de la API, si existe devuelve una respuesta Verdadero o Falso (True or False).
        /// </summary>
        /// <param name="id">(ID) Identificador de una Marca.</param>
        /// <returns></returns>
        private bool BrandExists(int id)
        {
            return _context.Brands.Any(b=>b.idBrand==id);
        }
    }
}