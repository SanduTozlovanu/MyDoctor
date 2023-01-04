/*!

=========================================================
* Argon Dashboard React - v1.2.2
=========================================================

* Product Page: https://www.creative-tim.com/product/argon-dashboard-react
* Copyright 2022 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/argon-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/

// reactstrap components
import {
  Button,
  Card,
  CardHeader,
  CardBody,
  FormGroup,
  Form,
  Input,
  Container,
  Row,
  Col,
} from 'reactstrap'
// core components
import UserHeader from 'components/Headers/UserHeader.js'
import { useUserContext } from 'context/UserContext'
import { useState, useEffect } from 'react'
import DoctorApi from 'api/doctor'
import PatientApi from 'api/patient'
import Swal from 'sweetalert2'
import AuthApi from 'api/auth'
import { useHistory } from 'react-router-dom'

const handleAccountTypeText = (text) => {
  const firstLetter = text.charAt(0).toUpperCase()
  const restOfWord = text.substring(1, text.length).toLowerCase()
  return firstLetter + restOfWord
}

const Profile = () => {
  const { user } = useUserContext()
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [accountType, setAccountType] = useState('')
  const [speciality, setSpeciality] = useState('')
  const [description, setDescription] = useState('A few words about you...')
  const [username, setUsername] = useState('')
  const history = useHistory()

  useEffect(() => {
    console.log(user)
    if (user && user.id) {
      setFirstName(user.firstName)
      setLastName(user.lastName)
      setEmail(user.email)
      setAccountType(user.accountType)
      if (user.accountType === 'DOCTOR') {
        setSpeciality(user.speciality)
      }
      setUsername(
        `${user.firstName.toLowerCase()}.${user.lastName.toLowerCase()}`,
      )
    }
  }, [user])

  const updateProfile = async () => {
    try {
      const data = {
          firstName: firstName,
          lastName: lastName,
          username: username,
          description: description,
      }
      if (accountType === 'DOCTOR') {
       const response =  await DoctorApi.UpdateDoctor(user.id, data)
       console.log(response)
       /* response va contine noile date si vor trebui setate in state */
      } else if(accountType === 'PATIENT'){
        const response  = await PatientApi.UpdatePatient(user.id, data)
        console.log(response)
      }
    } catch (error) {
      console.log(error)
    }
  }

  const deletePopup = async () => {
    const { value: password } = await Swal.fire({
      title: 'Sorry to see you go',
      text:
        'In order to permanently delete your account, please confirm your password.',
      icon: 'info',
      input: 'password',
      inputPlaceholder: 'Enter your password',
      inputAttributes: {
        maxlength: 30,
        autocapitalize: 'off',
        autocorrect: 'off',
      },
      confirmButtonText: 'Delete',
      showCloseButton: true
    })
    if (password) {
      try {
        await AuthApi.Login({
          email: user.email,
          password: password,
        })
        if (user.accountType === 'DOCTOR') {
          await DoctorApi.DeleteDoctor(user.id)
        } else if (user.accountType === 'PATIENT') {
          await PatientApi.DeletePatient(user.id)
        }
        history.push('/auth/logout')
      } catch (error) {
        return Swal.fire({
          title: 'Oops',
          text: 'Your password is wrong.',
          icon: 'error',
        })
      }
    }
  }

  return (
    <>
      <UserHeader firstName={firstName} lastName={lastName} />
      {/* Page content */}
      <Container className="mt--7" fluid>
        <Row>
          <Col className="order-xl-2 mb-5 mb-xl-0 d-flex" xl="4">
            <Card className="card-profile shadow flex-fill">
              <Row className="justify-content-center">
                <Col className="order-lg-2" lg="3">
                  <div className="card-profile-image">
                    <a href="#pablo" onClick={(e) => e.preventDefault()}>
                      <img
                        alt="..."
                        className="rounded-circle"
                        src={require('../../assets/img/theme/team-4-800x800.jpg')}
                      />
                    </a>
                  </div>
                </Col>
              </Row>
              <CardHeader className="text-center border-0 pt-8 pt-md-4 pb-0 pb-md-4"></CardHeader>
              <CardBody className="pt-0 pt-md-4">
                <Row>
                  <div className="col">
                    <div className="card-profile-stats d-flex justify-content-center mt-md-5">
                      <div>
                        <span className="heading">22</span>
                        <span className="description">Friends</span>
                      </div>
                      <div>
                        <span className="heading">10</span>
                        <span className="description">Photos</span>
                      </div>
                      <div>
                        <span className="heading">89</span>
                        <span className="description">Comments</span>
                      </div>
                    </div>
                  </div>
                </Row>
                <div className="text-center">
                  <h3>
                    {firstName} {lastName}
                  </h3>
                  <div className="h5 font-weight-300">
                    <i className="ni location_pin mr-2" />
                    Romania
                  </div>
                  <div className="h5 mt-4">
                    <i className="ni business_briefcase-24 mr-2" />
                    {handleAccountTypeText(accountType)}
                  </div>
                  <div>
                    <i className="ni education_hat mr-2" />
                    {speciality}
                  </div>
                  <hr className="my-4" />
                  <p>{description}</p>
                </div>
              </CardBody>
            </Card>
          </Col>
          <Col className="order-xl-1 d-flex" xl="8">
            <Card className="bg-secondary shadow flex-fill">
              <CardHeader className="bg-white border-0">
                <Row className="align-items-center">
                  <Col className="text-left">
                    <h2 className="mb-0">My account</h2>
                  </Col>
                  <Col className="text-right">
                    <Button
                      color="primary"
                      href="#pablo"
                      onClick={updateProfile}
                    >
                      Save Changes
                    </Button>
                  </Col>
                </Row>
              </CardHeader>
              <CardBody>
                <Form>
                  <h6 className="heading-small text-muted mb-4">
                    User information
                  </h6>
                  <div className="pl-lg-4">
                    <Row>
                      <Col lg="6">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-username"
                          >
                            Username
                          </label>
                          <Input
                            className="form-control-alternative"
                            defaultValue={username}
                            id="input-username"
                            placeholder="Username"
                            type="text"
                            onChange={(e) => setUsername(e.target.value)}
                          />
                        </FormGroup>
                      </Col>
                      <Col lg="6">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-email"
                          >
                            Email address
                          </label>
                          <Input
                            className="form-control-alternative"
                            defaultValue={email}
                            id="input-email"
                            placeholder="jesse@example.com"
                            type="email"
                            style={{ pointerEvents: 'none' }}
                          />
                        </FormGroup>
                      </Col>
                    </Row>
                    <Row>
                      <Col lg="6">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-first-name"
                          >
                            First name
                          </label>
                          <Input
                            maxLength={20}
                            className="form-control-alternative"
                            defaultValue={firstName}
                            id="input-first-name"
                            placeholder="First name"
                            type="text"
                            onChange={(e) => setFirstName(e.target.value)}
                          />
                        </FormGroup>
                      </Col>
                      <Col lg="6">
                        <FormGroup>
                          <label
                            className="form-control-label"
                            htmlFor="input-last-name"
                          >
                            Last name
                          </label>
                          <Input
                            maxLength={20}
                            className="form-control-alternative"
                            defaultValue={lastName}
                            id="input-last-name"
                            placeholder="Last name"
                            type="text"
                            onChange={(e) => setLastName(e.target.value)}
                          />
                        </FormGroup>
                      </Col>
                    </Row>
                  </div>
                  <hr className="my-4" />
                  {/* Description */}
                  <h6 className="heading-small text-muted mb-4">About me</h6>
                  <div className="pl-lg-4">
                    <FormGroup>
                      <label>About Me</label>
                      <Input
                        className="form-control-alternative"
                        placeholder="A few words about you ..."
                        rows="4"
                        defaultValue={description}
                        type="textarea"
                        onChange={(e) => setDescription(e.target.value)}
                      />
                    </FormGroup>
                  </div>
                  <hr className="my-4" />
                  <h4
                    onClick={deletePopup}
                    className="text-left mb-0 mt-4 text-danger c-pointer"
                  >
                    Delete my Account
                  </h4>
                </Form>
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </>
  )
}

export default Profile
