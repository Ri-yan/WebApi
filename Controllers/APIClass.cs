using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApplicationOne.Data;
using WebApplicationOne.Model;

namespace WebApplicationOne.Controllers
{
    /*CREATED BY : MOHD RIYAN
    CREATION DATE : 18-01-2023
    PURPOSE: TO LEARN BASIC CONCEPTS API
    UPDATED BY : 
    UPDATION DATE:
    PUROPOSE :
    */

    //to get full list
    [Route("api/villaAPI")]
    [ApiController]
    public class APIClass : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaModel>> getVillas()
        {

            return VillaStore._villaList;
        }
        //get request with id
        [HttpGet("{id:int}", Name = "getVilla")]
        //produces response type to be expected
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaModel> getVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = VillaStore._villaList.FirstOrDefault(u => u.intId == id);
                if (villa == null)
                {
                    return NotFound();
                }
                return Ok(villa);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaModel[]> addVilla([FromBody] VillaModel villaModel)
        {
            if (villaModel == null)
            {
                return BadRequest(villaModel);
            }
            if (villaModel.intId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaModel.intId = VillaStore._villaList.OrderByDescending(u => u.intId).FirstOrDefault().intId + 1;
            VillaStore._villaList.Add(villaModel);
            return CreatedAtRoute("getVilla", new { id = villaModel.intId }, villaModel);
        }
        //to delete villa
        [HttpDelete("{id:int}", Name = "deleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaModel[]> deleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore._villaList.FirstOrDefault(u => u.intId == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore._villaList.Remove(villa);
            return NoContent();
        }
        //to update villa
        [HttpPut("{id:int}", Name = "putVilla")]
        public ActionResult<VillaModel[]> updateVilla(int id,[FromBody]VillaModel villaModel)
        {
           if(villaModel==null || id!= villaModel.intId)
            {
                return BadRequest();
            }
            var villa = VillaStore._villaList.FirstOrDefault(u => u.intId == id);
            villa.strName = villaModel.strName;
            return NoContent();    
        }

    }
}
