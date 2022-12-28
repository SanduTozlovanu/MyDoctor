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
const PatientSurvey = () => {
  const { user } = useUserContext()
  const questions = ["Do you have diabetis?", "Do you have high blood pressure?", "Do / did you have cancer?", "Do you have any allergies?"]
  return (
    <>
      <Header />
      {/* Page content */}
      <Container className="mt--7" fluid>
        <Row>
          <Col>
            <Card className="shadow">
              <CardHeader className="border-0 pb-0">
                <h1 className="mb-0 text-center font-weight-700">
                  Complete this survey
                </h1>
              </CardHeader>
              <CardBody>
                <Row>
                  <Col className='mb-3'>
                    <h2>
                      Please answer to all questions by checking the right
                      answer for you.
                    </h2>
                  </Col>
                </Row>
                {questions && questions.length ? (questions.map((question, index) =>{
                    return (
                        <Row
                        key={index}>
                        <Col>
                          <h4>{question}</h4>
                        </Col>
                        <Col>
                          <label>
                            <input type="checkbox" className="mr-2"></input>
                            Yes
                          </label>
                        </Col>
                        <Col>
                          <label>
                            <input type="checkbox" className="mr-2"></input>
                            No
                          </label>
                        </Col>
                        <Col>
                          <label className="ws-0">
                            <input type="checkbox" className="mr-2"></input>I don't
                            know
                          </label>
                        </Col>
                      </Row>
                    )
                })) : null}
              </CardBody>
              <CardFooter className="border-0 pt-0">
                <Row>
                  <Col className="text-center">
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
