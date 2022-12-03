import React from 'react'
import { Redirect, Route } from 'react-router-dom'
import { useUserContext } from 'context/UserContext'

const ProtectedRoute = ({ component: Component, ...restOfProps }) => {
  const { user } = useUserContext()
  return (
    <Route
      {...restOfProps}
      render={(props) =>
        user && user.jwtToken ? (
          <Component {...props} />
        ) : (
          <Redirect to="/auth/login" />
        )
      }
    />
  )
}

export default ProtectedRoute
