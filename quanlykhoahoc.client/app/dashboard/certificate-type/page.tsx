"use client";

import DataTable from "../../../components/DataTable/DataTable";
import {
  CertificateTypeClient,
  CertificateTypeMapping,
} from "../../web-api-client";

export default function DashboardCertificateType() {
  const CertificateTypeService = new CertificateTypeClient();

  return (
    <DataTable
      url="/certificate-type"
      fields={Object.keys(new CertificateTypeMapping().toJSON())}
      deleteAction={(id) => CertificateTypeService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        CertificateTypeService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
