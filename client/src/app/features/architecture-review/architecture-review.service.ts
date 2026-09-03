import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ArchitectureReviewResult } from './architecture-review.models';

@Injectable({ providedIn: 'root' })
export class ArchitectureReviewService {
  private readonly http = inject(HttpClient);

  analyze(architectureDescription: string): Observable<ArchitectureReviewResult> {
    return this.http.post<ArchitectureReviewResult>(
      `${environment.apiBaseUrl}/api/architecture-review/analyze`,
      { architectureDescription },
    );
  }
}
