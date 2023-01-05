/* eslint-disable react-hooks/exhaustive-deps */
import Header from 'components/Headers/Header'
import {
  Button,
  Card,
  CardHeader,
  CardBody,
  Container,
  CardFooter,
  Row,
  Col,
} from 'reactstrap'
import { useUserContext } from 'context/UserContext'
import { useState, useEffect } from 'react'
import SurveyApi from 'api/survey'

const answers = ['Yes', 'No', "I don't know"]

const PatientSurvey = () => {
  const { user } = useUserContext()
  const [questions, setQuestions] = useState([
      'Do you have diabetis?',
      'Do you have high blood pressure?',
      'Do / did you have cancer?',
      'Do you have any allergies?'])
  const [response, setResponse] = useState([])

  const handleQuestionAnswer = (e, index) => {
    e.persist()
    setQuestions((current) =>
      current.map((obj) => {
        if (questions.indexOf(obj) === index) {
          return { ...obj, answer: e.target.value }
        }

        return obj
      }),
    )
  }

  useEffect(() => {
    const fetchData = async () => {
        await getQuestions(user.id)
    }
    fetchData()
}, [])


 const getQuestions = async () => {
  try{
    const response = await SurveyApi.GetQuestions(user.id);
    setQuestions(response.data)
    console.log(response.data)
  }catch(error){
    console.log(error)
  }
 }

  return (
    <>
      <Header />
      {/* Page content */}
      <Container className="mt--7" fluid>
        <Row>
          <Col>
            <Card className="shadow">
              <CardHeader className="bg-transparent">
                <h3 className="mb-0">Complete Survey</h3>
              </CardHeader>
              <CardBody>
                <Row>
                  <Col>
                    <h3>
                      Please answer all questions by checking the right answer
                      for you.
                    </h3>
                  </Col>
                </Row>
                {questions && questions.length
                  ? questions.map((question, index) => {
                      return (
                        <Row key={index} className="mt-3">
                          <Col>
                            <h3 className="font-weight-400">
                              {question}
                            </h3>
                            <Row className="text-left">
                              {answers.map((answer, answerIndex) => {
                                return (
                                  <Col key={answerIndex}>
                                    <label className="mb-0 ws-0">
                                      <input
                                        onChange={(e) =>
                                          handleQuestionAnswer(e, index)
                                        }
                                        type="radio"
                                        className="mr-2 mb-0"
                                        name={`answers-${index}`}
                                        value={answer}
                                      />
                                      {answer}
                                    </label>
                                  </Col>
                                )
                              })}
                            </Row>
                          </Col>
                        </Row>
                      )
                    })
                  : null}
              </CardBody>
              <CardFooter className="border-0 pt-0">
                <Row>
                  <Col className="text-right">
                    <Button color="primary" className="btn btn-lg">
                      Save survey
                    </Button>
                  </Col>
                </Row>
              </CardFooter>
            </Card>
          </Col>
        </Row>
      </Container>
    </>
  )
}

export default PatientSurvey
