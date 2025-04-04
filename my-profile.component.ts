import { HttpHeaders } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

uploadProfileImage(file: File) {
  const headers = new HttpHeaders({
    'Authorization': this.token  // Changed to match your other working requests
  });

  const formData = new FormData();
  formData.append('file', file);

  return this.http.post('http://localhost:8000/User/UploadProfileImage', formData, { headers })
    .pipe(
      catchError((error) => {
        console.error('Upload error:', error);
        return throwError(() => error);
      })
    );
} 