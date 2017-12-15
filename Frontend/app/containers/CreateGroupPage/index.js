/**
 *
 * CreateGroupPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { FormattedMessage } from 'react-intl';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import makeSelectCreateGroupPage from './selectors';
import reducer from './reducer';
import saga from './saga';
import messages from './messages';
import styled from 'styled-components';

import Header from 'components/Header';
import TextInput from 'components/TextInput';
import NumberInput from 'components/NumberInput';
import AddInput from 'components/AddInput';
import Select from 'components/Select';
import TextArea from 'components/TextArea';
import PriceInput from 'components/PriceInput';
import AddPersonInput from 'components/AddPersonInput';
import Switcher from 'components/Switcher';
import PrimaryBtn from 'components/PrimaryButton';
import SecondaryBtn from 'components/SecondaryButton';


const Wrapper = styled.div`
  padding: 0;
  width: 50%;
`

const Title = styled.div`
  display: block;
  text-align: center;
  margin: 20px 0;
`
const ButtonsWrapper = styled.div`
  display: flex;
  justify-content: space-around;
  align-items: center;
  margin: 30px 0;
  padding: 0 100px;
`

const Separator = styled.div`
  width: 100%;
  height: 1.5px;
  background-color:#9a9a9a;
  margin-bottom: 20px;
`

const Row = styled.div`
  display: flex;
  justify-content: flex-start;
  align-items: center;
  margin: 0;
`

const Label = styled.label`
  padding-left: 0;
  font-family: inherit;
  font-weight: inherit;
`

const typesOfStudy = [
  {
    name: 'Лекция',
    value: 'lection'
  },
  {
    name: 'Вебинар',
    value: 'webinar'
  }
]

const members = [

]

export class CreateGroupPage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <div>
        <header>
          <Header/>
        </header>
        <Wrapper className='container'>
          <Title className='row'>
            <h3>Создание группы</h3>
            <Separator/>
          </Title>
          <Row className='row'>
            <Label className='col-xs-4' for="name">Название группы</Label>
            <TextInput id='name'/>
          </Row>
          <Row className='row'>
            <Label className='col-xs-4' for="name">Человек в группе</Label>
            <NumberInput id='numberOfMembers'/>
          </Row>
          <Row className='row'>
            <Label className='col-xs-4' for="addTech">Изучаемые технологии</Label>
            <AddInput id='addTech'/>
          </Row>
          <Row className='row'>
            <Label className='col-xs-4' for="format">Формат занятий</Label>
            <Select id='format' options={typesOfStudy}/>
          </Row>
          <Row className='row'>
            <Label className='col-xs-4' for="text-area">Описание</Label>
            <TextArea id='text-area'/>
          </Row>
          <Row className='row'>
            <Label className='col-xs-4' for="price">Стоимость</Label>
            <PriceInput id='price'/>
          </Row>
          <Row className='row'>
            <Label className='col-xs-4' for="members">Добавить участника</Label>
            <AddPersonInput id='members' membersListForAdd={members}/>
          </Row>
          <Row className='row'>
            <Label className='col-xs-4' for="text-area">Приватная группа</Label>
            <Switcher id='type' leftOption={'Нет'} rightOption={'Да'}/>
          </Row>
          <ButtonsWrapper>
            <SecondaryBtn>Отмена</SecondaryBtn>
            <PrimaryBtn>Создать группу</PrimaryBtn>
          </ButtonsWrapper>
        </Wrapper>
      </div>
    );
  }
}

CreateGroupPage.propTypes = {
  dispatch: PropTypes.func.isRequired,
};

const mapStateToProps = createStructuredSelector({
  creategrouppage: makeSelectCreateGroupPage(),
});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'createGroupPage', reducer });
const withSaga = injectSaga({ key: 'createGroupPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(CreateGroupPage);
