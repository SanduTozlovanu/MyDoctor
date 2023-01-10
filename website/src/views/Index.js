import {
  Container,
  Row,
  Col,
  Card,
  CardHeader,
  Button,
  CardBody,
  Table,
} from 'reactstrap'
// core components
/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from 'react'
import Header from 'components/Headers/Header'
import { useUserContext } from "context/UserContext";
import Swal from 'sweetalert2'
import withReactContent from 'sweetalert2-react-content'
import DrugApi from 'api/drug';
const ReactSwal = withReactContent(Swal)

const Index = () => {

  return (
      <>
          <Header />
          {/* Page content */}
          <Container className="mt--7" fluid>
              {/* Table */}
              <Row>
                  <Col>
                      <Card className="shadow">
                          <CardBody className='text-center'>
                              <h2 className='mb-0'>Welcome, we are happy to have you here.</h2>
                              <img width={1100} height={500} src={require('../assets/img/dashboard/welcome-page-image.png')} alt="dashboard" />
                          </CardBody>
                      </Card>
                  </Col>
              </Row>
          </Container>
      </>
  )
}

export default Index