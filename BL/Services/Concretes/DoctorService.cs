using AutoMapper;
using BL.DTOs.DoctorDTOs;
using BL.Exceptions;
using BL.Services.Abstractions;
using CORE.Models;
using DAL.Repository.Absractions;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Concretes
{
    public class DoctorService : IDoctorService
    {
        readonly IDoctorRepository _doctorRepository;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _webHostEnvironment;
        public DoctorService(IWebHostEnvironment webHostEnvironment, IMapper mapper, IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task AddDoctorAsync(AddDoctorDTO doctorDTO)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorDTO);

            string rootPath = _webHostEnvironment.WebRootPath;
            string folder = rootPath + "/uploads/doctors/";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string fileName = doctorDTO.Image.FileName;

            string[] extensions = [".jpg", ".png", "jpeg"];
            bool isAllowed = false;
            foreach (var extension in extensions)
            {
                if (Path.GetExtension(fileName) == extension)
                {
                    isAllowed = true;
                    break;
                }
            }

            if (!isAllowed)
            {
                throw new FileNotAllowedException("File is not supported.");
            }

            string filePath = folder + fileName;

            using (FileStream stream = new FileStream(folder,FileMode.Create))
            {
                await doctorDTO.Image.CopyToAsync(stream);
            }
            doctor.ImageURL = filePath;

            await _doctorRepository.AddAsync(doctor);
            int result = await _doctorRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationCanceledException("Couldn't create doctor.");
            }
        }

        public async Task DeleteDoctorAsync(int Id)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(Id);
            if (doctor is null)
            {
                throw new ItemNotFoundException("Couldnt find item.");
            }
            _doctorRepository.Delete(doctor);
            int result = await _doctorRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationCanceledException("Couldn't delete doctor.");
            }
        }

        public async Task<ICollection<Doctor>> GetAllDoctors()
        {
            ICollection<Doctor> doctors = await _doctorRepository.GetAllAsync();
            return doctors;
        }

        public async Task<Doctor> GetDoctorByIdAsync(int Id)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(Id);
            if (doctor is null)
            {
                throw new ItemNotFoundException("Couldnt find doctor.");
            }
            return doctor;
        }

        public async Task UpdateDoctorAsync(UpdateDoctorDTO doctorDTO)
        {
            Doctor doctor = await _doctorRepository.GetByConditionAsync(d => d.Id==doctorDTO.Id);
            string rootPath = _webHostEnvironment.WebRootPath;
            string folder = rootPath + "/uploads/doctors/";
            string fileName = doctorDTO.Image.FileName;

            string[] extensions = [".jpg", ".png", "jpeg"];
            bool isAllowed = false;
            foreach (var extension in extensions)
            {
                if (Path.GetExtension(fileName) == extension)
                {
                    isAllowed = true;
                    break;
                }
            }

            if (!isAllowed)
            {
                throw new FileNotAllowedException("File is not supported.");
            }

            string filePath = folder + fileName;

            using (FileStream stream = new FileStream(folder, FileMode.Create))
            {
                await doctorDTO.Image.CopyToAsync(stream);
            }
            doctor.ImageURL = filePath;
            if (doctor is null)
            {
                throw new ItemNotFoundException("Couldnt find doctor.");
            }
            _doctorRepository.Update(doctor);
            int result = await _doctorRepository.SaveChangesAsync();
            if (result == 0)
            {
                throw new OperationCanceledException("Couldn't update doctor.");
            }
        }
    }
}
