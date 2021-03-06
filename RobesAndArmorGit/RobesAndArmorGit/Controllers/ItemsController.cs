﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameData;
using GameData.Models;
using System.IO;

namespace RobesAndArmorGit.Controllers
{
    public class ItemsController : Controller
    {
        private readonly GameContext _context;

        public ItemsController(GameContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            Models.ViewModels.itemCreateImages view = new Models.ViewModels.itemCreateImages();
            string path = Path.Combine(Environment.CurrentDirectory, @"wwwroot\images\item\");
            view.images = new List<string>();
            
            view.type = _context.Types.ToList();
            foreach (string difile in Directory.GetFiles(path))
            {

                view.images.Add(Path.GetFileName(difile));
            }

            return View(view);
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(string name,string def,string atk, string description, string itemimg,string level,string health, string type, string price)
        //public async Task<IActionResult> Create([Bind("Id,Name,Def,Atk")] Item item)        
        {
            GameData.Models.Item item = new Item();
            item.Atk = Convert.ToInt32(atk);
            item.Def = Convert.ToInt32(def);
            item.Level = Convert.ToInt32( level);
            item.Name = name;
            item.typeId = Convert.ToInt32(type);
            item.Health = Convert.ToInt32(health);
            item.price = Convert.ToInt32(price);
            item.imgeUrl = itemimg;

            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
        
        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            Models.ViewModels.itemEdit itemEdit = new Models.ViewModels.itemEdit();
            itemEdit.item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
            itemEdit.images = Logic.getImages.gettheImages("item");
            itemEdit.type = await _context.Types.ToListAsync();

            

            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(itemEdit);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Def,Atk")] Item item)
          public async Task<IActionResult> Edit(int id, string itemimg, string type, Item item )
        {
            /*
            if (id != item.Id)
            {
                return NotFound();
            }
            */
            if (ModelState.IsValid)
            {

                item.imgeUrl = itemimg;
                item.typeId = Convert.ToInt32(type);
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
