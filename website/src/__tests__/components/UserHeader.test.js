import React from 'react'
import { shallow } from 'enzyme'
import UserHeader from '../../components/Headers/UserHeader'
test('should test Header component', () => {
  const wrapper = shallow(<UserHeader />)
  expect(wrapper).toMatchSnapshot()
})
