import axios from './index'

class AppointmentApi {
 static async CreateAppointment(patientId, doctorId, data){
    return await axios.post(`/Appointments/${patientId}_${doctorId}/create_appointment`, data)
 }
}

export default AppointmentApi