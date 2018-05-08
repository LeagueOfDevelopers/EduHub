/**
 * NotFoundPage
 *
 * This is the page we show when the user visits a url that doesn't have a route
 *
 * NOTE: while this component should technically be a stateless functional
 * component (SFC), hot reloading does not currently support SFCs. If hot
 * reloading is not a necessity for you then you can refactor it and remove
 * the linting exception.
 */

import React from 'react';
import {Row, Col, Button} from 'antd';
import messages from './messages';
import {Link} from "react-router-dom";

export default class NotFound extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <Row type='flex' style={{height: 'calc(100vh - 60px)', alignItems: 'center'}}>
        <Col xs={{span: 20, offset: 2}} md={{span: 8, offset: 2}} className='md-text-center md-margin-top-30'>
          <h1 style={{fontWeight: 600}}>
            This page not found :(
          </h1>
          <Button type='primary' onClick={() => location.assign('/')}>Перейти на главную</Button>
        </Col>
        <Col xs={{span: 20, offset: 2}} md={{span: 10, offset: 2}}>
          <img style={{width: '100%', maxWidth: 550}} src={require('../../images/404_4.jpg')} alt=""/>
        </Col>
      </Row>
    );
  }
}
