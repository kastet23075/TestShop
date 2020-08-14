import React, { Fragment, useState } from 'react';
import { connect } from 'react-redux';

const axios = require('axios').default;
let email: string;
let password: string;
let confirmPassword: string;

let getEmailMessage = (): string => {
    let inputElement: HTMLInputElement = document.querySelector('.register-email') as HTMLInputElement;
    inputElement.classList.remove('input-validation-error', 'valid');
    email = inputElement.value.trim();
    let pattern = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+[\.[A-Z]{2,4}$]*/i;

    if (!email.length) {
        inputElement.classList.add('input-validation-error');

        return 'The Email field is required!';
    }

    if (pattern.test(email)) {
        inputElement.classList.remove('input-validation-error');
        inputElement.classList.add('valid');

        return '';
    }

    inputElement.classList.add('input-validation-error');

    return 'The Email field is not a valid e-mail address!';
}

let getPasswordMessage = (): string => {
    password = (document.querySelector('.register-password') as HTMLInputElement).value.trim();

    if (!password.length) {
        return 'The Password field is required!';
    }

    return password.length >= 6 ? '' : 'Password length must be at least 6 symbols';
}

let getConfirmPasswordMessage = (): string => {
    confirmPassword = (document.querySelector('.register-confirm-password') as HTMLInputElement).value.trim();

    return password.localeCompare(confirmPassword) ? 'Passwords do not match' : '';
}

function Register() {
    const [errorMessageForm, validationForm] = useState('');
    const [errorMessageEmail, validationEmail] = useState('');
    const [errorMessagePassword, validationPassword] = useState('');
    const [errorMessageConfirmPassword, validationConfirmPassword] = useState('');
    
    let handleSubmit = (event: any) => {
        if (!getEmailMessage().length && !getPasswordMessage().length && !getConfirmPasswordMessage().length) {
            validationForm('');
            axios.post('api/account/register',
                {
                    Email: email,
                    Password: password,
                    ConfirmPassword: confirmPassword
                },
                'json'
            )
            .then(() => {
                // There will be a redirect to the main page
            })
            .catch((exception: any) => {
                let inputElement: HTMLInputElement = document.querySelector('.register-email') as HTMLInputElement;
                inputElement.classList.remove('valid');
                inputElement.classList.add('input-validation-error');

                switch (exception.response.status) {
                    case 400:
                    {                        
                        validationForm('Invalid username or password!');
                        break;
                    }
                    case 422:
                    {
                        validationForm('This email is already in use!');
                        break;
                    }
                }
            });
        } else {
            validationEmail(getEmailMessage());
            validationPassword(getPasswordMessage());
            validationConfirmPassword(getConfirmPasswordMessage());
        }

        event.preventDefault();
    }

    return (
        <Fragment>
            <h1>Register</h1>

            <div className="row">
                <div className="col-md-4">
                    <form onSubmit={ handleSubmit }>
                        <h4>Create a new account.</h4>
                        <hr />
                        <div className="validation-summary-valid text-danger">{ errorMessageForm }</div>
                        <div className="form-group">
                            <label htmlFor="email">Email</label>
                            <div className="input-group">
                                <input
                                    className="form-control email-input register-email"
                                    type="email"
                                    onChange={ () => validationEmail(getEmailMessage()) }
                                    onBlur={ () => validationEmail(getEmailMessage()) }
                                />
                                <i className="email-info-icon fas"></i>
                            </div>
                            <span className="text-danger">{ errorMessageEmail }</span>
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input
                                className="form-control register-password"
                                type="password"
                                onBlur={ () => validationPassword(getPasswordMessage()) }
                            />
                            <span className="text-danger">{ errorMessagePassword }</span>
                        </div>
                        <div className="form-group">
                            <label htmlFor="confirmPassword">Confirm password</label>
                            <input
                                className="form-control register-confirm-password"
                                type="password"
                                onBlur={ () => validationConfirmPassword(getConfirmPasswordMessage()) }
                            />
                            <span className="text-danger">{ errorMessageConfirmPassword }</span>
                        </div>
                        <button type="submit" className="btn btn-primary">Register</button>
                    </form>
                </div>
            </div>
        </Fragment>
    );
}

export default connect()(Register);