import { createSelector } from 'reselect';

/**
 * Direct selector to the registrationAccepted state domain
 */
const selectRegistrationAcceptedDomain = (state) => state.get('registrationAccepted');

const makeSelectStatus = () => createSelector(
  selectRegistrationAcceptedDomain,
  (pageState) => pageState.get('status')
);


export {
  makeSelectStatus,
};
