/**
*
* GroupsFilterForm
*
*/

import React from 'react';

import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import { getFilteredGroups } from "../../containers/GroupsPage/actions";
import {Row, Col, Card, Input, Select, Radio, Checkbox, Divider, InputNumber} from 'antd';


class GroupsFilterForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      title: '',
      type: 'Default',
      formed: this.props.formed,
      tags: [],
      minPrice: 0,
      maxPrice: 10000
    };

    this.onHandleTitleChange = this.onHandleTitleChange.bind(this);
    this.onHandleFormedChange = this.onHandleFormedChange.bind(this);
    this.onHandleTagsChange = this.onHandleTagsChange.bind(this);
    this.onHandleTypeChange = this.onHandleTypeChange.bind(this);
    this.onHandleMinPriceChange = this.onHandleMinPriceChange.bind(this);
    this.onHandleMaxPriceChange = this.onHandleMaxPriceChange.bind(this);
  }

  componentDidMount() {
    this.props.getFilteredGroups(this.state);
  }

  onHandleTitleChange = (e) => {
    this.setState({title: e.target.value});
    setTimeout(() => this.props.getFilteredGroups(this.state), 0);
  };

  onHandleFormedChange = (e) => {
    this.setState({formed: e.target.checked});
    setTimeout(() => this.props.getFilteredGroups(this.state), 0);
  };

  onHandleTagsChange = (e) => {
    this.setState({tags: e});
    setTimeout(() => this.props.getFilteredGroups(this.state), 0);
  };

  onHandleTypeChange = (e) => {
    this.setState({type: e.target.value});
    setTimeout(() => this.props.getFilteredGroups(this.state), 0);
  };

  onHandleMinPriceChange = (e) => {
    this.setState({minPrice: e});
    setTimeout(() => this.props.getFilteredGroups(this.state), 0);
  };

  onHandleMaxPriceChange = (e) => {
    this.setState({maxPrice: e});
    setTimeout(() => this.props.getFilteredGroups(this.state), 0);
  };

  render() {
    return (
      <Col id={this.props.id} xs={{span: 24}} lg={{span: 7, offset: 2}} xl={{span: 6, offset: 2}} xxl={{span: 5, offset: 2}}>
        <Card
          hoverable
          className='without-border-bottom'
          style={{cursor: 'default', zIndex: 100}}
        >
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Название</div>
            <Input value={this.state.title} onChange={this.onHandleTitleChange}/>
          </Row>
          <Divider/>
          <Row>
            <Checkbox checked={this.state.formed} onChange={this.onHandleFormedChange}>Сформирована</Checkbox>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Тэги</div>
            <Select mode="tags" value={this.state.tags} onChange={this.onHandleTagsChange} defaultActiveFirstOption={false} style={{width: '100%'}}>
              <Select.Option value="html">html</Select.Option>
              <Select.Option value="css">css</Select.Option>
              <Select.Option value="js">js</Select.Option>
              <Select.Option value="c#">c#</Select.Option>
            </Select>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Тип обучения</div>
            <Radio.Group value={this.state.type} onChange={this.onHandleTypeChange}>
              <Radio value='Lecture' style={{display: 'block', lineHeight: '30px'}}>Лекция</Radio>
              <Radio value='Seminar' style={{display: 'block', lineHeight: '30px'}}>Семинар</Radio>
              <Radio value='MasterClass' style={{display: 'block', lineHeight: '30px'}}>Мастер-класс</Radio>
              <Radio value='Default' style={{display: 'block', lineHeight: '30px'}}>Не важно</Radio>
            </Radio.Group>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Стоимость</div>
            <InputNumber min={0} max={10000} value={this.state.minPrice} onChange={this.onHandleMinPriceChange} style={{ width: 80 }}/>
            <span style={{margin: '0 8px', fontSize: 14}}>–</span>
            <InputNumber min={0} max={10000} value={this.state.maxPrice} onChange={this.onHandleMaxPriceChange} style={{ width: 80 }}/>
          </Row>
        </Card>
      </Col>
    );
  }
}

GroupsFilterForm.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    getFilteredGroups: (filters) => dispatch(getFilteredGroups(filters))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(GroupsFilterForm);

