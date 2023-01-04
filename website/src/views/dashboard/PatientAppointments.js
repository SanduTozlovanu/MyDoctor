import {
    Container,
    Row,
    Col,
    Card,
    CardHeader,
    Button,
    CardBody,
    Table
} from 'reactstrap'
// core components
/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from 'react'
import Header from 'components/Headers/Header'
import { useUserContext } from "context/UserContext";
import AppointmentApi from 'api/appointments';
import moment from 'moment';

const PatientAppointments = () => {

    const [error, setError] = useState('')
    const [appointments, setAppointments] = useState([
        {
            date: "2023-01-04",
            doctorFirstName: "Lavinia",
            doctorLastName: "Naet",
            email: "lavinian@gmail.com",
            startTime: "09:00",
            endTime: "10:00"
        },
        {
            date: "2023-01-04",
            doctorFirstName: "Theodor",
            doctorLastName: "Nae",
            email: "tnae@gmail.com",
            startTime: "08:00",
            endTime: "09:00"
        },
        {
            date: "2023-01-05",
            doctorFirstName: "Theodora",
            doctorLastName: "Scott",
            email: "scottThe@gmail.com",
            startTime: "08:00",
            endTime: "09:00"
        },
    ])
    const { user } = useUserContext()

    useEffect(() => {
        const fetchData = async () => {
            await getAppointments()
        }
        fetchData()
    }, [])

    useEffect(() => {
        appointments.sort((a, b) => new Date(...a.date.split('-').reverse()) - new Date(...b.date.split('-').reverse()));
        appointments.sort((a, b) => a.date.split(":") - b.date.split(":"));
    }, [])
    const getAppointments = async () => {
        try {
            // const response = await AppointmentApi.GetAppointments(user.id)
            // console.log(response)
            // setAppointments(response)
        } catch (error) {
            console.log(error)
            setError("Error: Couldn't get the appointments.")
        }
    }

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
                                            View your appointments
                                        </h3>
                                    </Col>
                                </Row>
                            </CardHeader>
                            <CardBody>
                                <Row>
                                    <Table className="align-items-center table-flush" responsive>
                                        <thead className="thead-light">
                                            <tr>
                                                <th scope="col">Date</th>
                                                <th scope="col">Time</th>
                                                <th scope="col">Doctor</th>
                                                <th scope="col">Email</th>
                                                <th scope='col'>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {error ? <tr><td><h4 className='text-left text-muted'>Sorry, we couldn't get the appointments list.</h4></td></tr> : null}
                                            {appointments.map((appointment, index) => {
                                                return (
                                                    <tr key={index}>
                                                        <th scope="row">
                                                            <span className="mb-0 text-sm">
                                                                {moment(appointment.date).diff(new Date().toISOString().slice(0, 10), 'day') === 0 ? "Today" : moment(appointment.date).diff(new Date().toISOString().slice(0, 10), 'day') === 1 ? "Tomorrow" : appointment.date}
                                                            </span>
                                                        </th>
                                                        <td>
                                                            <span className="mb-0 text-sm">
                                                                {appointment.startTime} - {appointment.endTime}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <span className="mb-0 text-sm">
                                                                {appointment.doctorFirstName} {appointment.doctorLastName}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <span className="mb-0 text-sm">
                                                                {appointment.email}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <Button className="btn btn-danger btn-sm">
                                                                Cancel
                                                            </Button>
                                                        </td>
                                                    </tr>)
                                            })}
                                        </tbody>
                                    </Table>
                                </Row>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </>
    )
}

export default PatientAppointments
