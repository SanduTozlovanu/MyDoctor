import axios from './index'

class AppointmentApi {
  static async CreateAppointment(patientId, doctorId, data) {
    return await axios.post(
      `/Appointments/${patientId}_${doctorId}/create_appointment`,
      data,
    )
  }
  static async GetAppointments(doctorId) {
    return await axios.get(`/Appointments/${doctorId}`)
  }
}

export default AppointmentApi
