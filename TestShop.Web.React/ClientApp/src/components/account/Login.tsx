import React, { Fragment, useState } from 'react';
import { connect } from 'react-redux';

const axios = require('axios').default;
let email: string;
let password: string;

let getEmailMessage = (): string => {
    email = (document.querySelector('.login-email') as HTMLInputElement).value.trim();
    let pattern = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+[\.[A-Z]{2,4}$]*/i;

    if (!email.length) {
        return 'Enter your email address!';
    }

    return pattern.test(email) ? '' : 'Invalid email address!';
}

let getPasswordMessage = (): string => {
    password = (document.querySelector('.login-password') as HTMLInputElement).value.trim();

    if (!password.length) {
        return 'Enter your password!';
    }

    return password.length >= 6 ? '' : 'Password length must be at least 6 symbols';
}

function Login() {
    
    const [errorMessageForm, validationForm] = useState('');
    const [errorMessageEmail, validationEmail] = useState('');
    const [errorMessagePassword, validationPassword] = useState('');

    let handleSubmit = (event: any) => {

        if (!getEmailMessage().length && !getPasswordMessage().length) {
            validationForm('');
            let remember: boolean = (document.querySelector('.login-remember') as HTMLInputElement).checked;

            axios.post('api/account/login',
                {
                    Email: email,
                    Password: password,
                    Remember: remember
                },
                'json'
            )
            .then((token: any) => {
                // There will be a redirect to the main page
                console.log(token.data.value.access_token);
            })
            .catch((exception: any) => {
                switch (exception.response.status) {
                    case 400:
                    case 404:
                    {
                        validationForm('Invalid username or password!');
                        break;
                    }
                    case 423:
                    {
                        validationForm('User with this email address is blocked!');
                        break;
                    }
                }
            });
        } else {
            validationEmail(getEmailMessage());
            validationPassword(getPasswordMessage());
        }

        event.preventDefault();
    }

    return (
        <Fragment>
            <h1>Log in</h1>

            <div className="row">
                <div className="col-md-4">
                    <form onSubmit={ handleSubmit }>
                        <h4>Use a local account to log in.</h4>
                        <hr />
                        <div className="validation-summary-valid text-danger">{ errorMessageForm }</div>
                        <div className="form-group">
                            <label htmlFor="email">Email</label>
                            <input
                                className="form-control login-email"
                                name="email"
                                type="email"
                                onBlur={ () => validationEmail(getEmailMessage()) }
                            />
                            <span className="text-danger">{ errorMessageEmail }</span>
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input
                                className="form-control login-password"
                                name="password"
                                type="password"
                                onBlur={ () => validationPassword(getPasswordMessage()) }
                            />
                            <span className="text-danger">{ errorMessagePassword }</span>
                        </div>
                        <div className="form-group">
                            <input type="checkbox" className="login-remember" id="remember" />
                            <label htmlFor="remember">Remember</label>
                        </div>
                        <div className="form-group">
                            <button type="submit" className="btn btn-primary">Log in</button>
                        </div>
                        <div className="form-group">
                            <p>
                                <a href="login">Forgot your password?</a>
                            </p>
                        </div>
                        <div className="form-group">
                            <p>
                                <a href="register">Register as a new user</a>
                            </p>
                        </div>
                    </form>
                </div>
            </div>
        </Fragment>
    );
}

export default connect()(Login);