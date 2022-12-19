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
import { useState, useEffect } from 'react'
import DoctorApi from 'api/doctor'
import Header from 'components/Headers/Header'

// function getDayName(date = new Date()) {
//   const locale = 'en-US'
//   return date.toLocaleDateString(locale, { weekday: 'long' })
// }

// function addDay(nrOfDays) {
//   let date = new Date()
//   date.setDate(date.getDate() + nrOfDays)
//   return date
// }

const DoctorSchedule = () => {
  const [days, setDays] = useState([
    {dayName: "Monday",  start: '', end: ''},
    {dayName: "Tuesday",  start: '', end: ''},
    {dayName: "Wednesday",  start: '', end: ''},
    {dayName: "Thursday",  start: '', end: ''},
    {dayName: "Friday",  start: '', end: ''},
    {dayName: "Saturday",  start: '', end: ''},
    {dayName: "Sunday",  start: '', end: ''},
  ])

  const updateState = (index ,target , value) => {
    setDays(days.map(day => {
      if (days.indexOf(day) === index) {
        if (target === "start"){
            return {...day, start: value};
        } else if (target === "end"){
            return {...day, end: value};
        }
      }
      return day;
    }))
  };
  const sendSchedule = async () => {
    try {
     const data = days;
     await DoctorApi.SendSchedule(data)
    } catch (error) {
      console.log(error)
    }
  }

useEffect(() => {
  console.log(days)
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
                                <h2>{day.dayName}</h2>
                                <hr />
                                <Row>
                                  <Col  xl="6" lg="12" md="12" sm="6" xs="6" className="text-left">
                                    <Label>Start time</Label>
                                    <Input
                                      defaultValue={day.start}
                                      onChange={(e) => updateState(index, "start", e.target.value)}
                                      type="time"
                                    />
                                  </Col>
                                  <Col xl="6" lg="12" md="12" sm="6" xs="6" className="text-left">
                                    <Label>End time</Label>
                                    <Input
                                      defaultValue={day.end}
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
