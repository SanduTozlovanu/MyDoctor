import React from 'react'
import { shallow } from 'enzyme'
import Header from '../../components/Headers/Header'
test('should test Header component', () => {
  const wrapper = shallow(<Header />)
  expect(wrapper).toMatchSnapshot()
})
