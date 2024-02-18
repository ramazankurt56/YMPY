using AutoMapper;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;
namespace HospitalServer.Business.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<CreateAppointmentDto, Appointment>();
        CreateMap<CreateExaminationDto, Examination>();
        CreateMap<CreateMedicationDto, Medication>();
        CreateMap<CreatePatientDto, Patient>();
        CreateMap<CreatePrescriptionDto, Prescription>();


        CreateMap<UpdateDoctorDto, Doctor>();
        CreateMap<UpdateAppointmentDto, Appointment>();
        CreateMap<UpdateExaminationDto, Examination>();
        CreateMap<UpdateMedicationDto, Medication>();
        CreateMap<UpdatePatientDto, Patient>();
        CreateMap<UpdatePrescriptionDto, Prescription>();


    }
}
