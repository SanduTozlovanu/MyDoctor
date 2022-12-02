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
import React, { createContext, useState, useEffect } from 'react'

import { BrowserRouter, Route, Switch, Redirect } from 'react-router-dom'

import 'assets/plugins/nucleo/css/nucleo.css'
import '@fortawesome/fontawesome-free/css/all.min.css'
// eslint-disable-next-line
import 'assets/scss/argon-dashboard-react.scss'

import AdminLayout from 'layouts/Admin.js'
import AuthLayout from 'layouts/Auth.js'

const UserContext = createContext()

const App = () => {
  const [user, setUser] = useState(null)

  useEffect(() => {
    let userLocal = localStorage.getItem("user");
    if (userLocal && typeof userLocal === 'string') {
      userLocal = JSON.parse(userLocal)
      setUser(userLocal)
    }
  }, [])

  return (
    <UserContext.Provider value={user}>
      <BrowserRouter>
        <Switch>
          <Route path="/admin" render={(props) => <AdminLayout {...props} />} />
          <Route path="/auth" render={(props) => <AuthLayout {...props} />} />
          <Redirect from="/" to="/admin/index" />
        </Switch>
      </BrowserRouter>
    </UserContext.Provider>
  )
}

export default App
