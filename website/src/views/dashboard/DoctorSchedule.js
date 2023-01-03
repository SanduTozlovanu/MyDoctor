import {
  Container,
  Row,
  Col,
  Card,
  CardHeader,
  Label,
  CardFooter,
  CardBody,
  Input,
  Button,
} from 'reactstrap'
// core components
/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from 'react'
import Header from 'components/Headers/Header'
import { useUserContext } from "context/UserContext";
import ScheduleApi from 'api/schedules';

const DoctorSchedule = () => {
 
  const [days, setDays] = useState([
    {id: "", dayOfWeek: "Monday",  startTime: '', endTime: ''},
    {id: "", dayOfWeek: "Tuesday",  startTime: '', endTime: ''},
    {id: "", dayOfWeek: "Wednesday",  startTime: '', endTime: ''},
    {id: "", dayOfWeek: "Thursday",  startTime: '', endTime: ''},
    {id: "", dayOfWeek: "Friday",  startTime: '', endTime: ''},
    {id: "", dayOfWeek: "Saturday",  startTime: '', endTime: ''},
    {id: "", dayOfWeek: "Sunday",  startTime: '', endTime: ''},
  ])
  const [error, setError] = useState(null)
  const { user } = useUserContext()

  useEffect(() => {
    const fetchData = async () => {
      await getSchedule()
    }
    fetchData()
  }, [])

  const getSchedule = async () => {
    try {
      const response = await ScheduleApi.GetSchedule(user.id)
      setDays(response.data)
    } catch (err) {
      setError("Server error.")
    }
  }
  const updateState = (index ,target , value) => {
    setDays(days.map(day => {
      if (days.indexOf(day) === index) {
        if (target === "start"){
            return {...day, startTime: value};
        } else if (target === "end"){
            return {...day, endTime: value};
        }
      }
      return day;
    }))
  };
  
  const sendSchedule = async () => {
    try {
      const data = days.map(item => {
        return {
          id: item.id,
          startTime: item.startTime,
          endTime: item.endTime
        }
      })
     await ScheduleApi.SendSchedule(data)
     await getSchedule();
    } catch (error) {
      setError("Server error.")
    }
  }

useEffect(() => {
  console.log("useeffect days", days)
  setError(null)
}, [days])

  return (
    <>
      <Header />
      {/* Page content */}
      <Container className="mt--7" fluid>
        {/* Table */}
        <Row>
          <Col>
            <Card className="shadow">
              <CardHeader>
                <Row className="align-items-center">
                  <Col lg="8" md="8" sm="12" xs="12" className="text-left">
                    <h3 className="mb-0">
                      Set your schedule for the current week
                    </h3>
                  </Col>               
                  <Col lg="4" md="4" sm="12" xs="12" className="text-right">
                    <Button onClick={sendSchedule} color="primary">Save Schedule</Button>
                    {error ? (
                    <h4 className="text-right text-danger mt-3 font-weight-400">
                      {error}
                    </h4>
                  ) : null}
                  </Col>
                </Row>
              </CardHeader>
              <CardBody>
                <Row>
                  {days.map((day, index) => {
                    return (
                      <Col
                        lg="4"
                        md="6"
                        sm="6"
                        xs="12"
                        className="mb-4"
                        key={index}
                      >
                        <Card className="bg-secondary shadow">
                          <CardBody>
                            <Row>
                              <Col className="text-center">
                                <h2>{day.dayOfWeek}</h2>
                                <hr />
                                <Row>
                                  <Col  xl="6" lg="12" md="12" sm="6" xs="6" className="text-left">
                                    <Label>Start time</Label>
                                    <Input
                                      defaultValue={day.startTime}
                                      onChange={(e) => updateState(index, "start", e.target.value)}
                                      type="time"
                                    />
                                  </Col>
                                  <Col xl="6" lg="12" md="12" sm="6" xs="6" className="text-left">
                                    <Label>End time</Label>
                                    <Input
                                      defaultValue={day.endTime}
                                      onChange={(e) => updateState(index, "end", e.target.value)}
                                      type="time"
                                    />
                                  </Col>
                                </Row>
                              </Col>
                            </Row>
                          </CardBody>
                        </Card>
                      </Col>
                    )
                  })}
                </Row>
              </CardBody>
              <CardFooter className="border-0 pt-0">
                <Row>
                  <Col className="text-center"></Col>
                </Row>
              </CardFooter>
            </Card>
          </Col>
        </Row>
      </Container>
    </>
  )
}

export default DoctorSchedule
