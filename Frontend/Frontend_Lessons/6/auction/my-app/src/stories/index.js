import React from 'react';
import '../uikit.css';
import '../index.css';
import { storiesOf } from '@storybook/react';
import { action } from '@storybook/addon-actions';
import { linkTo } from '@storybook/addon-links';

import PrimaryBtn from '../components/primary-btn';
import DefaultBtn from '../components/default-btn';
import SuccessBtn from '../components/success-btn';
import DangerBtn from '../components/danger-btn';
import SearchBar from '../components/search-bar';
import Card from '../components/card';
import Header from '../components/header';
import InputSumForm from '../components/inputSumForm';
import RegistrationForm from '../components/registrationForm';
import CardInfo from '../components/card-info';
import LoginForm from '../components/loginForm';

storiesOf('Buttons', module)
.add('primary', () => (
  <PrimaryBtn title = "Primary" />
))
.add('default', () => (
  <DefaultBtn title = "Default" />
))
.add('success', () => (
  <SuccessBtn title = "Success" />
))
.add('danger', () => (
  <DangerBtn title = "Danger" />
));

storiesOf('Elements of page', module)
.add('header', () => (
  <Header />
))
.add('big search', () => (
  <SearchBar />
))
.add(' single card', () => (
  <Card />
))
.add(' full card', () => (
  <CardInfo />
));

storiesOf('Forms', module)
.add('input sum', () => (
    <InputSumForm />
))
.add('registration', () => (
    <RegistrationForm />
))
.add('login', () => (
    <LoginForm />
));