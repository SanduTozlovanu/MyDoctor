import axios from './index'

class SurveyApi {
  static async GetQuestions(patientId) {
    return await axios.get(`/SurveyQuestions/${patientId}`)
  }
}

export default SurveyApi