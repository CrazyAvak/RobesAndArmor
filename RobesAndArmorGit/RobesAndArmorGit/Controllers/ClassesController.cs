﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameData;
using GameData.Models;
using Microsoft.AspNetCore.Authorization;

namespace RobesAndArmorGit.Controllers
{
    public class ClassesController : Controller
    {
        private readonly GameContext _context;

        public ClassesController(GameContext context)
        {
            _context = context;
        }

        // GET: Classes
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Classes.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Models.ViewModels.classesCreate classesCreate = new Models.ViewModels.classesCreate();
            classesCreate.images = Logic.getImages.gettheImages("classimg");
            classesCreate.charClass = new Class();

            return View(classesCreate);
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Str,Agility,Int")] Class @class)
          public async Task<IActionResult> Create(string Name, string Str, string Agility, string Int, string classimg)
        {
            GameData.Models.Class @class = new Class();
            @class.Name = Name;
            @class.Str = Convert.ToInt32(Str);
            @class.Agility = Convert.ToInt32(Agility);
            @class.Int = Convert.ToInt32(Int);
            @class.imageUrl = classimg;

            if (ModelState.IsValid)
            {
                
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {

            

            if (id == null)
            {
                return NotFound();
            }
            Models.ViewModels.classesCreate classesCreate = new Models.ViewModels.classesCreate();
            classesCreate.charClass = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
            if(classesCreate.charClass.imageUrl == null)
            {
                classesCreate.charClass.imageUrl = "";
            }
            classesCreate.images = Logic.getImages.gettheImages("classimg");
            

            if (classesCreate.charClass == null)
            {
                return NotFound();
            }
            return View(classesCreate);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Str,Agility,Int")] Class @class)
        public async Task<IActionResult> Edit(int id,string classimg , Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                @class.imageUrl = classimg;
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
